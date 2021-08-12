using System;
using Frame.Utility;
using Game.flag;
using Photon.Pun;
using UnityEngine;

public abstract class PointBase : MonoBehaviourPun,IPoint,IPunInstantiateMagicCallback
{
    public abstract bool IsOccupied();
    [NonSerialized]
    public int rowIndex;
    [NonSerialized]
    public int columnIndex;

    public string GetOccupiedSign()
    {
        return gameObject.name;
    }
    
    [PunRPC]
    public void ChangeOccupiedSign(string sign)
    {
        ChangeTo(sign);
        PointJudge.Instance.Change(rowIndex,columnIndex,sign);
    }

    public void Reset()
    {
        ChangeTo(FlagData.Instance.GetUnoccupiedSignName());
        PointJudge.Instance.JustChange(rowIndex,columnIndex,FlagData.Instance.GetUnoccupiedSignName());
    }

    private void ChangeTo(string sign)
    {
        string path = ConfigurationManager.Instance.GetPathByName(sign);
        GameObject point = PhotonNetwork.Instantiate(path,transform.position, Quaternion.identity, 0,
            new object[] {rowIndex, columnIndex});
        point.transform.parent = transform.parent;
        PhotonNetwork.Destroy(gameObject);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //初始化
        object[] data = info.photonView.InstantiationData;
        rowIndex = (int)data[0];
        columnIndex = (int)data[1];
    }
}
