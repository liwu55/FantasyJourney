using System;
using DG.Tweening;
using Frame.UI;
using Frame.Utility;
using Game;
using Game.bean;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EventType = Frame.Utility.EventType;

public class LoginModule : UIModuleBase
{
    private InputField inputName;
    private InputField inputPsw;
    private EventSystem _system;
    private bool isSelectUsername; //选框用户名
    private Image FocusLigthUsername;
    private Image FocusLigthPassword;
    private Text txtMention; //密码错误提示

    //可选对象
    Selectable cur = null;

    private void Start()
    {
        _system = EventSystem.current;
        txtMention = FW("txtMention#").Text;
        inputName = FW("Field-UserName#").InputField;
        inputPsw = FW("Field-UserPassword#").InputField;
        FW("LoginButton#").Button.onClick.AddListener(() =>
        {
            if (inputName.text == "")
            {
                return;
            }
            Debug.Log("点击了登录按钮 name=" + inputName.text + " psw=" + inputPsw.text);
            //调用UIEvent的事件
            UIEvent.LoginClick(inputName.text, inputPsw.text);
        });    
    }

    
    public void PlayMentionAnime()
    {

        Debug.Log("PlayMentionAnime");
        Tweener _tweener = txtMention.DOFade(1, 0.6f);
        
        _tweener.SetDelay(0);
        _tweener.SetEase(Ease.Linear);
        _tweener.SetAutoKill(true);
        _tweener.SetLoops(0);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = null;
            cur = _system.currentSelectedGameObject.GetComponent<Selectable>();
            print("name=" + cur.name);
            if (cur.name == "Field-UserName#")
            {
                isSelectUsername = true;
                next = cur.FindSelectableOnDown();
            }
            else
            {
                isSelectUsername = false;
                next = cur.FindSelectableOnUp();
            }

            _system.SetSelectedGameObject(next.gameObject, new BaseEventData(_system));
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIEvent.LoginClick(inputName.text, inputPsw.text);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //一键清空登录状态
            EventCenter.Instance.Call(EventType.ResetLogin);
        }
    }
}