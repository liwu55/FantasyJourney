using System;
using ExitGames.Client.Photon;
using Frame.Utility;
using Game.flag.State;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class BFFHeroController : SimpleHeroController
{
    //private Vector3 hitBackVelocity;

    public int Score = 0;
    // Start is called before the first frame update
  // protected override void FixedUpdate()
  // {
  //     if (!photonView.IsMine)
  //     {
  //         return;
  //     }
  //     base. FixedUpdate();
  //     //被击退
  //     if(hitBackVelocity.magnitude>0.1f){
  //         cc.SimpleMove(hitBackVelocity);
  //         hitBackVelocity = Vector3.Lerp(hitBackVelocity, Vector3.zero, Time.deltaTime * 6);
  //     }
  // }

    [PunRPC]
    private void OnTriggerEnter( Collider IbffItem)
    {
        if (IbffItem.CompareTag("Badges"))
        {
            Score += 1;
            PhotonNetwork.Destroy(IbffItem.gameObject);
           
        }
        
        if (IbffItem.CompareTag("Dice"))
        {
            Score += Random.Range(1,7);
            PhotonNetwork.Destroy(IbffItem.gameObject);
        }
        
        if (IbffItem.CompareTag("Gift"))
        {
            Score += 20;
            PhotonNetwork.Destroy(IbffItem.gameObject);
        }
        
    }
}
