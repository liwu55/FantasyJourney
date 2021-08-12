using ExitGames.Client.Photon;
using Game.flag;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class FlagPhotonCallBack : MonoBehaviourPunCallbacks
{
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer == PhotonNetwork.LocalPlayer)
        {
            if (changedProps.ContainsKey(PhotonPlayerWrap.TEAM))
            {
                string teamName = (string)changedProps[PhotonPlayerWrap.TEAM];
                UIManager.Instance.ShowModule("FlagGaming", teamName);
            }
        }
    }
}
