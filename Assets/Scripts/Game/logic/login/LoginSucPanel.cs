using System.ComponentModel.Design;
using Frame.UI;
using Game;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Object = System.Object;

public class LoginSucPanel : UIModuleBase
{
   private Button BtnCheck;
   private Text LoginMsg;
   private string usernameStr;
   protected override void Awake()
   {
      base.Awake();
      BtnCheck = FW("BtnCheck#").Button;
      LoginMsg = FW("txtLoginMsg#").Text;
      
   }

   
   public void Init()
   {
      LoginMsg.text = "亲爱的" + usernameStr + ",欢迎进入游戏";
      BtnCheck.onClick.AddListener(() =>
      {
         //进入游戏
         
      });
      
      
   }

   public override void OnSpawn(Object obj)
   {
      base.OnSpawn(obj);
      usernameStr = (string)obj;
      Debug.Log("usernameStr="+usernameStr);
      Init();
   }
}
