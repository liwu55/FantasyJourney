using System;
using Game.bean;
using Game.flag;
using Photon.Pun;
using UnityEngine;

public class ControllerInit : MonoBehaviour,IPunInstantiateMagicCallback
{
    [NonSerialized]public bool inFlag;
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] data = info.photonView.InstantiationData;
        int sign = (int) data[0];
        inFlag = sign == 0;
        //插旗游戏
        if (inFlag)
        {
            if (PlayerInfo.Instance.chooseHero.id == 0)
            {
                gameObject.AddComponent<NiuNiuHeroController>();
            }
            else
            {
                gameObject.AddComponent<FlagHeroController>();
            }
        }
        else
        {
            gameObject.AddComponent<BFFHeroController>();
        }
       
    }
}
