using System;
using DG.Tweening;
using Frame.UI;
using Game;
using Game.bean;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginModule : UIModuleBase
{
    private InputField inputName;
    private InputField inputPsw;
    private EventSystem _system;
    private bool isSelectUsername;//选框用户名
    private Image FocusLigthUsername;
    private Image FocusLigthPassword;
    private Text txtMention; //密码错误提示
  
    //可选对象
    Selectable cur = null;
    private void Start()
    {
        _system = EventSystem.current;
        txtMention = FW("txtMention#").Text;
        inputName=FW("Field-UserName#").InputField;
        inputPsw=FW("Field-UserPassword#").InputField;
        FW("LoginButton#").Button.onClick.AddListener(() =>
        {
            Debug.Log("点击了登录按钮 name="+inputName.text+" psw="+inputPsw.text);
            //调用UIEvent的事件
            UIEvent.LoginClick(inputName.text,inputPsw.text);
        });
    }

    public void PlayMentionAnime()
    {
      Tweener _tweener = txtMention.DOFade(1,2);
      _tweener.SetDelay(0);
      _tweener.SetEase(Ease.Linear);
      _tweener.SetAutoKill(true);
      _tweener.SetLoops(0);
      Tweener tweener = txtMention.DOFade(0, 1.5f);
      tweener.SetEase(Ease.Linear);
      tweener.SetLoops(0);
      tweener.SetDelay(2.1f);
    }
    /*public string GetUsernameStr()
    {
       UserInfo _user =  DataBaseManager.Instance.GetUserInfo(inputName.text, inputPsw.text);
       return _user.username;
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = null;
            cur = _system.currentSelectedGameObject.GetComponent<Selectable>();
            print("name="+cur.name);
            if (cur.name=="Field-UserName#")
            {
                isSelectUsername = true;
                next = cur.FindSelectableOnDown();
            }
            else
            {
                isSelectUsername = false;
                next = cur.FindSelectableOnUp();
            }
            _system.SetSelectedGameObject(next.gameObject,new BaseEventData(_system));
        }
    }

 
}
