namespace Frame.Utility
{
    public interface PoolObject
    {
        void OnSpawn();
        void OnRecycle();
        void OnPause();
        void OnResume();
    }
}