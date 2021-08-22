
using System.Collections.Generic;
using Frame.FSM;
using Frame.UI;
using Frame.Utility;
using Game;
using Game.flag;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class BFFUI : UIModuleBase
{
    private Text time;
    public float curTime;
    public int AllTime = 60;
    private Button backButton;
    private bool loadScene=false;
    int champScore = 0;
    string champName;
    protected override void Awake()
    {
        base.Awake();
        backButton = transform.Find("BFFGameOverPanel/BackButton").GetComponent<Button>();
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            SceneHeroes.Instance.Clear();
            UIManager.Instance.Clear();
            ObjectPool.Instance.Clear();
            MonoHelper.Instance.Clear();
            PhotonNetwork.LeaveRoom();
            Time.timeScale = 1;
        });
        time = transform.Find("Text").GetComponent<Text>();
        curTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - curTime > 1.0f)
        {
            AllTime--;
            curTime =Time.time;
            time.text = AllTime.ToString();

            if (AllTime <= 0)
            {
                Time.timeScale = 0;
                showPanel();
            }
        }
        
        if (!loadScene && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        {
            loadScene = true;
            SceneManager.LoadScene("SampleScene");
        }
        
         

       

    }

    void showPanel()
    {
        transform.Find("BFFGameOverPanel").gameObject.SetActive(true);
        getChampScore();
        transform.Find("BFFGameOverPanel/First/ScoreText").GetComponent<Text>().text =champScore.ToString();
        transform.Find("BFFGameOverPanel/First/ChampName").GetComponent<Text>().text = champName;
    }

    void getChampScore()
    {
        
        List<IHeroController> heroControllers = SceneHeroes.Instance.GetAll();
        List<BFFHeroController> bffHeroControllers = new List<BFFHeroController>();
        foreach (var item in heroControllers)
        {
            bffHeroControllers.Add((BFFHeroController)item);
        }

        for (int i = 0; i < bffHeroControllers.Count-1; i++)
        {
            if (bffHeroControllers[i].Score < bffHeroControllers[i + 1].Score)
            {
                champScore =  bffHeroControllers[i+1].Score;
                champName =  bffHeroControllers[i + 1].GetPhotonView().Owner.NickName;
            }
            else
            {
                champScore =  bffHeroControllers[i].Score;
                champName = bffHeroControllers[i].GetPhotonView().Owner.NickName;
            }
        }
        
    }
}
