using Frame.Utility;
using UnityEngine;

public class AutoRecyclePoolObject : MonoBehaviour,PoolObject
{
    public float lifeTime = 3;
    public void OnSpawn(object obj)
    {
        Invoke("RecycleSelf",lifeTime);
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
