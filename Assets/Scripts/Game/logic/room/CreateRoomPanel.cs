using System;
using ExitGames.Client.Photon;
using Frame.UI;
using Frame.Utility;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class CreateRoomPanel : UIModuleBase
{
    private UIWidget roomNameInput;
    private UIWidget roomPwdInput;
    private void Start()
    {
        roomNameInput =  FW("RoomNameInputField#");
        roomPwdInput = FW("RoomPwdInputField#");
        SetRoomDefaultData();
        
        //创建房间按钮
        FW("SureButton#").Button.onClick.AddListener(() =>
        {
            UIManager.Instance.PopModule();
            RoomOptions options = new RoomOptions
            {
                //房间人数
                MaxPlayers = 6,
                //设置大厅属性
                CustomRoomPropertiesForLobby = new []{"RoomPassword"},
                //房间密码
                CustomRoomProperties = new Hashtable {
                {
                    "RoomPassWord",
                    roomPwdInput.InputField.text
                }}  
            };
            
            //创建房间r
            PhotonNetwork.CreateRoom(roomNameInput.InputField.text, options);
        });
        
        //返回按钮
        FW("BackButton#").Button.onClick.AddListener(() =>
        {
            UIManager.Instance.PopModule();
            
        });
    }
    

    private void SetRoomDefaultData()
    {
        
        roomNameInput.InputField.text = "Room"+Random.Range(0,100);
    }
}
