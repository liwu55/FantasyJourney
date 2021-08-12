using System.Collections;
using Game.flag;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TeamInit : MonoBehaviour
{
    private IEnumerator Start()
    {
        Debug.Log("TeamInit Start");
        //先等1秒再分队
        yield return new WaitForSeconds(1);
        Debug.Log("分队");
        //房主分队
        if (PhotonNetwork.IsMasterClient)
        {
            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {
                string teamName = i % 2 == 0 ? FlagData.Instance.GetFirstTeam() : FlagData.Instance.GetSecondTeam();
                new PhotonPlayerWrap(players[i]).SetTeam(teamName);
            }
        }
    }
}