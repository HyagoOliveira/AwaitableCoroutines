# Awaitable Coroutines

* Temporary solution to easily run Async code in Unity
* Unity minimum version: **2019.3**
* Current version: **0.1.0**
* License: **MIT**

## Summary

Currently, Unity **does not support** a proper way to run **async/await** code without using **Coroutines**. 

This will change in the next [big update for the Unity .NET](https://blog.unity.com/technology/unity-and-net-whats-next#:~:text=Editor%20during%202024.-,Modernizing%20the%20Unity%20runtime,-.NET%20Standard%202.1) 
where asynchronous programing will improve significantly. <br/>
Until there, **this package provides a simple way to execute asynchronous code**. 
Use it until Unity provides the official solution.

## How To Use

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