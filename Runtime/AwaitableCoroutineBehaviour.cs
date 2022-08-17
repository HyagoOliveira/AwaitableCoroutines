using UnityEngine;

namespace ActionCode.AwaitableCoroutines
{
    /// <summary>
    /// Internal component to execute coroutines inside <see cref="AwaitableCoroutine"/>.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class AwaitableCoroutineBehaviour : MonoBehaviour
    {
        private void OnDestroy()
        {
            StopAllCoroutines();
            AwaitableCoroutine.StopAllTasks();
        }
    }
}