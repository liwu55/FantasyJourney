using Frame.FSM;
using Frame.UI;
using Frame.Utility;
using Game.flag;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class BFFUI : UIModuleBase
{
    private Text time;
    public int curTime;
    public int AllTime = 5;
    private Button backButton;
    private bool loadScene=false;
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
        curTime = (int)Time.time;
    }

    private void Update()
    {
        if ((int) Time.time - curTime > 1)
        {
            AllTime--;
            curTime = (int) Time.time;
            time.text = AllTime.ToString();

            if (AllTime <= 0)
            {
                Time.timeScale = 0;
                //UIManager.Instance.ShowModule("BFFGameOverPanel");
                transform.Find("BFFGameOverPanel").gameObject.SetActive(true);
            }
        }
        
        if (!loadScene && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        {
            loadScene = true;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
