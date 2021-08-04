using System;

namespace Frame.Utility
{
    public interface PoolObject
    {
        void OnSpawn(Object obj);
        void OnRecycle();
        void OnPause();
        void OnResume();
    }
}