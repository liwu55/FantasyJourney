using System;
using System.Collections.Generic;
using System.Text;
using Frame.UI;
using Game;
using Game.bean;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MainModule : UIModuleBase
{
    private ClientState previousState;


    private UIWidget lobbyButton;
    private UIWidget playerName;
    private UIWidget bagButton;//背包（选择英雄）
    private UIWidget settingButton;//设置
    private UIWidget quitButton;//退出
    private UIWidget moneyText;//金币
    private UIWidget crownText;//皇冠
    private UIWidget playerNowLevleFA;
    private UIWidget playerLevelText;
    
    private UIWidget rankingList;
    [SerializeField] private GameObject rankingListImage;
    private UIWidget listName1;
    private UIWidget listName2;
    private UIWidget listName3;
    private UIWidget listName4;
    private UIWidget listName5;
    private List<UIWidget> listName=new List<UIWidget>();
    private StringBuilder str=new StringBuilder();
    private bool rankingListBool=false;
    
    [SerializeField]private GameObject settingPanel;
    private bool settingPanelBool=true;
    private UIWidget settingBack1;
    private UIWidget settingBack2;
    
    private GameObject quitPanel;
    private bool quitPanelBool=true;
    private UIWidget quitBack1;
    private UIWidget quitBack2;
    private UIWidget quitBack3;
    private UIWidget quit;
    
    private MainPageInfo info;

    protected override void Awake()
    {
        base.Awake();
        playerName = FW("PlayerName#");
        lobbyButton = FW("LobbyButton#");
        bagButton = FW("BagButton#");
        settingButton = FW("SettingButton#");
        quitButton = FW("QuitButton#");
        moneyText = FW("MoneyText#");
        crownText = FW("CrownText#");
        playerNowLevleFA = FW("PlayerNowLevleFA#");
        playerLevelText = FW("PlayerLevelText#");
        
        rankingList = FW("RankingList#");
        listName1 = FW("ListName1#");
        listName2 = FW("ListName2#");
        listName3 = FW("ListName3#");
        listName4 = FW("ListName4#");
        listName5 = FW("ListName5#");
        listName.Add(listName1);
        listName.Add(listName2);
        listName.Add(listName3);
        listName.Add(listName4);
        listName.Add(listName5);
        
        settingBack1 = FW("SettingBack1#");
        settingBack2 = FW("SettingBack2#");

        quitPanel = FW("QuitPanel#").gameObject;
        quitBack1 = FW("QuitBack1#");
        quitBack2 = FW("QuitBack2#");
        quitBack3 = FW("QuitBack3#");
        quit = FW("Quit#");

        playerName.Text.text = PhotonNetwork.LocalPlayer.NickName;
        
        lobbyButton.Button.onClick.AddListener(() =>
        {
            setNickName();
            PhotonNetwork.JoinLobby();
        });
        //背包
        bagButton.Button.onClick.AddListener(() =>
        {
            UIEvent.StoreClick();
        });
        //设置
        settingButton.Button.onClick.AddListener(() =>
        {
            showSetting();
        });
        settingBack1.Button.onClick.AddListener(() =>
        {
            showSetting();
        });
        settingBack2.Button.onClick.AddListener(() =>
        {
            showSetting();
        });
        //退出
        quitButton.Button.onClick.AddListener(() =>
        {
            showQuit();
        });
        quitBack1.Button.onClick.AddListener(() =>
        {
            showQuit();
        });
        quitBack2.Button.onClick.AddListener(() =>
        {
            showQuit();
        });
        quitBack3.Button.onClick.AddListener(() =>
        {
            showQuit();
        });
        quit.Button.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        //排名
        rankingList.Button.onClick.AddListener(() =>
        {
            //showList();
        });
        playerLevelText.Text.text = "0/100";
        playerNowLevleFA.Img.fillAmount = 0.0f;
        UIEvent.RefreshMainPageHero += showMainMoney;
        showSetting();
        showQuit();
        
    }

    private void OnDestroy()
    {
        UIEvent.RefreshMainPageHero -= showMainMoney;
    }


    public override void OnSpawn(object obj)
    {
        base.OnSpawn(obj);
        info=obj as MainPageInfo;
        if (info == null)
        {
            return;
        }
        UserInfo userInfo = info.userInfo;
        moneyText.Text.text = userInfo.money.ToString();
        crownText.Text.text = userInfo.honor.ToString();
    }

    public void showMainMoney()
    {
        UserInfo userInfo = info.userInfo;
        moneyText.Text.text = userInfo.money.ToString();
        crownText.Text.text = userInfo.honor.ToString();
    }

    public void showSetting()
    {
        if (!settingPanelBool)
        {
            settingPanel.SetActive(true);
            settingPanelBool = true;
        }
        else
        {
            settingPanel.SetActive(false);
            settingPanelBool = false;
        }
    }

    public void showQuit()
    {
        if (!quitPanelBool)
        {
            quitPanel.SetActive(true);
            quitPanelBool = true;
        }
        else
        {
            quitPanel.SetActive(false);
            quitPanelBool = false;
        }
    }
    //判断范围，未使用
    public void isRotate(RectTransform rectTransform)
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition)) return;
    }

    public void showList()
    {
        if (!rankingListBool)
        {
            rankingListImage.SetActive(true);
            writeList();
            rankingListBool = true;
        }
        else
        {
            rankingListImage.SetActive(false);
            rankingListBool = false;
        }
    }

    public void writeList()
    {
        List<UserInfo> userInfos = DataBaseManager.Instance.GetRankList(5);
        for (int i = 0; i < 5; i++)
        {
            str.Clear();
            str.Append((i+1).ToString());
            str.Append(" ， ");
            str.Append(userInfos[i].username);
            str.Append(" ， ");
            str.Append(userInfos[i].honor);
            listName[i].Text.text = str.ToString();
        }
    }

    private void Update()
    {

        if (PhotonNetwork.NetworkClientState
            != previousState)
        {
            Debug.Log("现在状态是:" + PhotonNetwork.NetworkClientState);
        }
        previousState = PhotonNetwork.NetworkClientState;


    }

    void setNickName()
    {
        //设置昵称
        //PhotonNetwork.LocalPlayer.NickName = playerName.Text.text;
        Debug.Log("我的昵称是:" + PhotonNetwork.LocalPlayer.NickName);

        //设置英雄
    }
}