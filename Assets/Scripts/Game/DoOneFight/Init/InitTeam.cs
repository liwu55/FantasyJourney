using System;
using System.Collections;
using Game.DoOneFight.Init;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class InitTeam : MonoBehaviour
{
   private IEnumerator Start()
   {
      yield return new WaitForSeconds(1);
      if (PhotonNetwork.IsMasterClient)
      {
         Player[] Players = PhotonNetwork.PlayerList;
         new CustomPlayerTeam(Players[0]).SetTeam("Blue");
         new CustomPlayerTeam(Players[1]).SetTeam("Red");
      }
   }
}
