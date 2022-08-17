using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ActionCode.AwaitableCoroutines
{
    /// <summary>
    /// Static class that can asynchronous run coroutines.
    /// </summary>
    public static class AwaitableCoroutine
    {
        private static AwaitableCoroutineBehaviour Behaviour => behaviour.Value;

        private static readonly HashSet<TaskCoroutine> currentTasks = new HashSet<TaskCoroutine>();
        private static readonly Lazy<AwaitableCoroutineBehaviour> behaviour = new Lazy<AwaitableCoroutineBehaviour>(CreateBehaviour);

        /// <summary>
        /// Runs the given routine and returns a Task object that represents that work.
        /// </summary>
        /// <param name="routine">The routine to execute asynchronously.</param>
        /// <param name="cancellationToken">The cancellation token that allow the routine to be canceled.</param>
        /// <returns>A task that represents the routine queued to execute in the thread pool.</returns>
        public static async Task Run(IEnumerator routine, CancellationToken cancellationToken = default)
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("Cannot run routine out of Play Mode!");
                return;
            }

            var task = new TaskCoroutine(routine);
            task.OnComplete += HandleTaskCompleted;

            currentTasks.Add(task);

            await task.Run(cancellationToken);
        }

        internal static void StopAllTasks()
        {
            foreach (var task in currentTasks)
                task.Cancel();

            currentTasks.Clear();
        }

        private static void HandleTaskCompleted(TaskCoroutine task) => currentTasks.Remove(task);

        private static AwaitableCoroutineBehaviour CreateBehaviour()
        {
            var name = typeof(AwaitableCoroutineBehaviour).Name;
            return GetOrCreateNotEditableGameObject<AwaitableCoroutineBehaviour>(name);
        }

        private static T GetOrCreateNotEditableGameObject<T>(string name) where T : Component
        {
            var component = Object.FindObjectOfType<T>(includeInactive: true);
            var hasComponent = component != null;
            var gameObject = hasComponent ?
                component.gameObject :
                new GameObject();

            gameObject.name = name;
            gameObject.hideFlags = HideFlags.NotEditable;

            Object.DontDestroyOnLoad(gameObject);

            return hasComponent ? component : gameObject.AddComponent<T>();
        }

        private class TaskCoroutine
        {
            public event Action<TaskCoroutine> OnComplete;

            private bool isRunning;
            private readonly IEnumerator coroutine;

            public TaskCoroutine(IEnumerator coroutine)
            {
                isRunning = false;
                this.coroutine = coroutine;
            }

            public async Task Run(CancellationToken cancellationToken)
            {
                var coroutine = Behaviour.StartCoroutine(RunCoroutine());

                while (isRunning)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Behaviour.StopCoroutine(coroutine);
                        break;
                    }
                    await Task.Yield();
                }

                OnComplete?.Invoke(this);
            }

            public void Cancel() => isRunning = false;

            private IEnumerator RunCoroutine()
            {
                isRunning = true;
                yield return coroutine;
                isRunning = false;
            }
        }
    }
}