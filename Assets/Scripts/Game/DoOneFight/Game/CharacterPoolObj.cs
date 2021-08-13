using Frame.Utility;
using UnityEngine;


public class CharacterPoolObj : MonoBehaviour ,PoolObject
{
    public void OnSpawn(object obj)
    {
        transform.position = (Vector3) obj;
    }

    public void OnRecycle()
    {
        throw new System.NotImplementedException();
    }

    public void OnPause()
    {
        throw new System.NotImplementedException();
    }

    public void OnResume()
    {
        throw new System.NotImplementedException();
    }
}
