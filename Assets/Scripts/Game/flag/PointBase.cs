using Frame.Utility;
using Game.flag;
using Photon.Pun;
using UnityEngine;
using Object = System.Object;

public abstract class PointBase : MonoBehaviourPun,IPoint,PoolObject,IPunInstantiateMagicCallback
{
    public abstract bool IsOccupied();
    private int rowIndex;
    private int columnIndex;

    public string GetOccupiedSign()
    {
        return gameObject.name;
    }
    
    public void ChangeOccupiedSign(string sign)
    {
        ObjectPool.Instance.RecycleObj(gameObject);
        Object[] data={transform.position,rowIndex,columnIndex};
        ObjectPool.Instance.SpawnObj(sign, null, data);
        PointJudge.Instance.Change(rowIndex,columnIndex,sign);
    }

    public void OnSpawn(Object data)
    {
        //这里暂时没用了
        /*Object[] datas = (Object[]) data;
        transform.position = (Vector3)datas[0];
        rowIndex = (int)datas[1];
        columnIndex = (int)datas[2];*/
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

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //初始化
        object[] data = info.photonView.InstantiationData;
        rowIndex = (int)data[0];
        columnIndex = (int)data[1];
        Debug.Log("point 初始化 row="+rowIndex+" column="+columnIndex);
    }
}
