using System.ComponentModel.Design;
using Frame.UI;
using Game;
using Game.bean;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Object = System.Object;

public class LoginSucPanel : UIModuleBase
{
   private Button BtnCheck;
   private Text LoginMsg;
   protected override void Awake()
   {
      base.Awake();
      BtnCheck = FW("BtnCheck#").Button;
      LoginMsg = FW("txtLoginMsg#").Text;
      
      BtnCheck.onClick.AddListener(() =>
      {
         UIEvent.ToMain();
      });
    }

   
   public void Init()
   {
      LoginMsg.text = "亲爱的" +  PhotonNetwork.NickName + ",欢迎进入游戏！";
   }

   public override void OnSpawn(Object obj)
   {
      base.OnSpawn(obj);
      //设置photon昵称
      PhotonNetwork.NickName =  (string)obj;
      Init();
   }
}
