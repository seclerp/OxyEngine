using System;

namespace Oxy.Framework.OpenTK.Compatibility
{
    interface IPoolable : IDisposable
    {
        void OnAcquire();
        void OnRelease();
    }

    interface IPoolable<T> : IPoolable where T : IPoolable<T>, new()
    {
        ObjectPool<T> Owner { get; set;  }
    }
}
