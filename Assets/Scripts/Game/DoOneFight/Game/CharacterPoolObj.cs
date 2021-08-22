using Frame.Utility;
using UnityEngine;


public class CharacterPoolObj : MonoBehaviour ,PoolObject
{
    public void OnSpawn(object obj)
    {
        if(obj!=null){
            transform.position = (Vector3) obj;
        }
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
