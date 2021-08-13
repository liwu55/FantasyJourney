using System;
using System.Collections;
using System.Collections.Generic;
using Frame.UI;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ChatInGame :UIModuleBase
{
   private InputField ChatInputField;
   private Text ChatText;
   private List<string> ChatContent = new List<string>();
   private string chatMsg;
   private bool isOpenChatView;
   
   protected override void Awake()
   {
      base.Awake();
      ChatInputField = FW("InputFieldChat#").InputField;
      print(ChatInputField);
      ChatText = FW("TextChat#").Text;
      print(ChatText);
   }

   public void SendChatMsg()
   {
      chatMsg = ChatInputField.text;
      if (!string.IsNullOrEmpty(chatMsg))
      {
         photonView.RPC("Chat", RpcTarget.All, chatMsg);
         chatMsg = "";
         ChatInputField.text = "";
        
      }
   }

   [PunRPC]
   public void Chat(string msg , PhotonMessageInfo _messageInfo)
   {
      string senderName = "不愿透露姓名的玩家";
      ChatText.text = "";
      if (_messageInfo.Sender!=null)
      {
         if (!string.IsNullOrEmpty(_messageInfo.Sender.NickName))
         {
            senderName = _messageInfo.Sender.NickName;
         }
         else
         {
            senderName = "Player" + _messageInfo.Sender.UserId;
         }
         ChatContent.Add("<color=#EEAD0E>" + senderName +":</color> " + msg);
      }
      
      List<string> newmessages = new List<string>();
      if (ChatContent.Count > 24)
      {
         for (int i = (ChatContent.Count - 24); i < ChatContent.Count; i++)
         {
            newmessages.Add(ChatContent[i]);
         }
 
         for (int i = 0; i < newmessages.Count; i++)
         {
            ChatText.text += newmessages[i] + "\n";
         }
      }
      else
      {
         for (int i = 0; i < ChatContent.Count; i++)
         {
            ChatText.text += ChatContent[i] + "\n";
         }
      }
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Return))
      {
         if (!isOpenChatView)
         {
            isOpenChatView = true;
            ChatInputField.transform.GetComponent<CanvasGroup>().alpha = 1;
            ChatInputField.interactable = true;
            //自动进入激活状态
            ChatInputField.ActivateInputField();
         }
         else
         {
            isOpenChatView = false;
            ChatInputField.interactable = false;
            //发送信息
            SendChatMsg();
            ChatInputField.transform.GetComponent<CanvasGroup>().alpha = 0;
            
         }
      }
      
   }
}
