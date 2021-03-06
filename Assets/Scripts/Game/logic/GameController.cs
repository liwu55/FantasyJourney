using Game;
using Game.bean;
using Game.Interface;
using Photon.Pun;

//游戏控制类。负责所有游戏逻辑总控制
public class GameController: SingleTonObj<GameController>
{
    private MainPageInfo _mainPageInfoCache;
    private GameController(){}
    
    //具体的游戏逻辑控制，按功能分
    
    //UI控制
    private IUIController _uiController;
    //登录管理
    private ILoginManager _loginManager;
    //玩家数据
    private PlayerInfo _playerInfo;
    //地图
    private IMapManager _mapManager;
    //网络框架
    private IPhotonWrapper _photonWrapper;

    private bool init = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (init)
        {
            return;
        }

        init = true;
        InitObj();
        BindUIEvent();
    }

    /// <summary>
    /// 初始化各个逻辑对象的具体类
    /// </summary>
    private void InitObj()
    {
        _uiController=new UIController();
        //_loginManager=new LoginManager();
        _loginManager = new FakeLoginManager();
        _mapManager= new TestMapManager();
        _photonWrapper=new TestPhotonWrapper();
        _playerInfo=new PlayerInfo();
    }

    private void BindUIEvent()
    {
        UIEvent.LoginClick += Login;
        UIEvent.ToMain += ToMain;
        UIEvent.StoreClick += Store;
        UIEvent.SettingClick += Setting;
        UIEvent.MapClick += MapDetail;
        UIEvent.LobbyClick += Lobby;
        UIEvent.QuickStartClick += QuickStart;
        UIEvent.HeroChange += HeroChange;
        UIEvent.GameStart += GameStart;
    }

    private void ToMain()
    {
        _uiController.ShowMain(_mainPageInfoCache);
    }

    private void GameStart(MapInfo mapInfo)
    {
        //游戏开始，根据地图配置，跳到各自的场景
    }

    private void QuickStart()
    {
        _photonWrapper.RandomJoin((result) =>
        {
            if (result)
            {
                _uiController.ShowRoomInfo();
            }
            else
            {
                _photonWrapper.CreateRoom();
            }
        });
    }

    private void Lobby()
    {
        _uiController.ShowLobby();
    }

    private void MapDetail(MapInfo obj)
    {
        _uiController.ShowMapDetail();
    }

    private void Setting()
    {
        _uiController.ShowSetting();
    }

    private void Store()
    {
        _uiController.ShowStore();
    }

    private void HeroChange(HeroInfo heroInfo)
    {
        _playerInfo.chooseHero = heroInfo;
    }

    private void Login(string name, string pwd)
    {
        _loginManager.Login(name,pwd, (loginResult) =>
        {
            if (loginResult.suc)
            {
                _mainPageInfoCache = new MainPageInfo();
                _mainPageInfoCache.userInfo = loginResult.userInfo;
                
                PlayerInfo.Instance.Init();
                PlayerInfo.Instance._userInfo = loginResult.userInfo;
                _mainPageInfoCache.maps = _mapManager.GetAllMap();
                _uiController.ShowLoginSuc(_mainPageInfoCache.userInfo.username);
            }
            else
            {
                _uiController.ShowLoginError();
            }
        });
    }

    /// <summary>
    /// 游戏入口，游戏启动的时候调用
    /// </summary>
    public void Entrance()
    {
        if (PlayerInfo.Instance._userInfo != null)
        {
            _mainPageInfoCache = new MainPageInfo();
            _mainPageInfoCache.userInfo = PlayerInfo.Instance._userInfo;
            _mainPageInfoCache.maps = _mapManager.GetAllMap();
            _uiController.ShowMain(_mainPageInfoCache);
        }else{
            _uiController.ShowLogin();
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }
}
