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
    private UIController _uiController;

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
            //UIManager.Instance.ShowModule("Store");
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
        moneyText.Text.text = "";//当前金币
        crownText.Text.text = "";//当前皇冠数
        playerLevelText.Text.text = "0/100";
        playerNowLevleFA.Img.fillAmount = 0.0f;
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
        PlayerInfo.Instance.Init();
        PlayerInfo.Instance._userInfo = info.userInfo;
        moneyText.Text.text = userInfo.money.ToString();
        crownText.Text.text = userInfo.honor.ToString();
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