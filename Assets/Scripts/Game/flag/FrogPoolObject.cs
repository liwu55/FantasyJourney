using Frame.Utility;
using UnityEngine;

public class FrogPoolObject : MonoBehaviour,PoolObject
{
    public void OnSpawn(object obj)
    {
        Invoke("RecycleSelf",3);
    }

    private void RecycleSelf()
    {
        ObjectPool.Instance.RecycleObj(gameObject);
    }

    public void OnRecycle()
    {
    }

    public void OnPause()
    {
    }

    public void OnResume()
    {
    }
}
