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
    [SerializeField]
    private GameObject rankingListImage;
    private UIWidget listName1;
    private UIWidget listName2;
    private UIWidget listName3;
    private UIWidget listName4;
    private UIWidget listName5;
    private List<UIWidget> listName=new List<UIWidget>();
    private StringBuilder str=new StringBuilder();
    private bool showBool=false;

    private MainPageInfo info;

    protected override void Awake()
    {
        base.Awake();
    /*}

    private void Start()
    {*/
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
            UIManager.Instance.ShowModule("");
        });
        //退出
        quitButton.Button.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowModule("");
        });
        //排名
        rankingList.Button.onClick.AddListener(() =>
        {
            showList();
        });
        playerLevelText.Text.text = "0/100";
        playerNowLevleFA.Img.fillAmount = 0.0f;
        UIEvent.RefreshMainPageHero += showMainMoney;
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

    public void showList()
    {
        if (!showBool)
        {
            rankingListImage.SetActive(true);
            writeList();
            showBool = true;
        }
        else
        {
            rankingListImage.SetActive(false);
            showBool = false;
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
            str.Append(userInfos[4-i].money);
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