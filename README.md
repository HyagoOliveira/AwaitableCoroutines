# Awaitable Coroutines

* Temporary solution to easily run Coroutines asynchronously.
* Unity minimum version: **2019.3**
* Current version: **0.1.0**
* License: **MIT**

## Summary

Currently, Unity **does not support** a proper way to run Coroutines asynchronously.

This will change in the next [big update for the Unity .NET environment](https://blog.unity.com/technology/unity-and-net-whats-next#:~:text=Editor%20during%202024.-,Modernizing%20the%20Unity%20runtime,-.NET%20Standard%202.1) 
where asynchronous programing will be improved significantly. <br/>
Until there, **this package provides a simple way to execute Coroutines asynchronous** and improve your code legibility. 
Use it until Unity provides the official solution.

## How To Use

Simply import the namespace `ActionCode.AwaitableCoroutines` and use the asynchronous `AwaitableCoroutine.Run()` function to run any `Coroutine` in your code.

```csharp
using UnityEngine;
using System.Collections;
using ActionCode.AwaitableCoroutines;

public sealed class Test : MonoBehaviour
{
    public async void StartCount()
    {
        await AwaitableCoroutine.Run(CountUntil(5));
        print("Counting until five has finished!");

        await AwaitableCoroutine.Run(CountUntil(10));
        print("Counting until ten has finished!");
    }

    IEnumerator CountUntil(int seconds)
    {
        var waitOneSecond = new WaitForSeconds(1);
        for (int i = 1; i <= seconds; i++)
        {
            print("Counting: " + i);
            yield return waitOneSecond;
        }
    }
}
```

> **Note**: your class does not need to be a `MonoBehaviour`.

## How It Works

The static `AwaitableCoroutine` class uses a GameObject with the internal [AwaitableCoroutineBehaviour](/Runtime/AwaitableCoroutineBehaviour.cs) script to wrap and execute asynchronous tasks.

This GameObject is _Lazy_ created, i.e. it'll be only created when used for the first time and it'll be sent to the special scene **DontDestroyOnLoad**. After that it'll be cached.

![Awaitable Coroutine Behaviour in Inspector](/Docs~/AwaitableCoroutineBehaviour-Inspector.png "AwaitableCoroutineBehaviour in Inspector").

> For safety, this GameObject cannot be edited using the Inspector.

## Installation

### Using the Package Registry Server

Follow the instructions inside [here](https://cutt.ly/ukvj1c8) and the package **ActionCode-Awaitable Coroutines** 
will be available for you to install using the **Package Manager** windows.

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set. 

- Use the **Package Manager** "Add package from git URL..." feature and paste this URL: `https://github.com/HyagoOliveira/AwaitableCoroutines.git`

- You can also manually modify you `Packages/manifest.json` file and add this line inside `dependencies` attribute: 

```json
"com.actioncode.awaitable-coroutines":"https://github.com/HyagoOliveira/AwaitableCoroutines.git"
```

---

**Hyago Oliveira**

[GitHub](https://github.com/HyagoOliveira) -
[BitBucket](https://bitbucket.org/HyagoGow/) -
[LinkedIn](https://www.linkedin.com/in/hyago-oliveira/) -
<hyagogow@gmail.com>