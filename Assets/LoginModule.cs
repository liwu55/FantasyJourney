using System;
using Frame.UI;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class LoginModule : UIModuleBase
{
    private InputField inputName;
    private InputField inputPsw;
    private void Start()
    {
        inputName=FW("Field-UserName#").InputField;
        inputPsw=FW("Field-UserPassword#").InputField;
        FW("LoginButton#").Button.onClick.AddListener(() =>
        {
            Debug.Log("点击了登录按钮 name="+inputName.text+" psw="+inputPsw.text);
            //调用UIEvent的事件
            UIEvent.LoginClick(inputName.text,inputPsw.text);
        });
    }
}
