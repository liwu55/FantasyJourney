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
            FlagHeroController controller = gameObject.AddComponent<FlagHeroController>();
            if (PlayerInfo.Instance.chooseHero.id == 0)
            {
                controller.AddExtra(new NiuNiuHeroExtra());
            }
        }
        else
        {
            BFFHeroController controller = gameObject.AddComponent<BFFHeroController>();
            if (PlayerInfo.Instance.chooseHero.id == 0)
            {
                controller.AddExtra(new NiuNiuHeroExtra());
            }
        }
       
    }
}
