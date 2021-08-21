using Frame.FSM;
using Frame.UI;
using Frame.Utility;
using Game.flag;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlagOverModule : UIModuleBase
{
    private Text winTeamShow;
    private Text reward;
    protected override void Awake()
    {
        base.Awake();
        winTeamShow=FW("WinTeamShow#").Text;
        reward=FW("Reward#").Text;
        FW("Back#").Button.onClick.AddListener(()=>
        {
            SceneHeroes.Instance.Clear();
            UIManager.Instance.Clear();
            ObjectPool.Instance.Clear();
            FlagData.Instance.Clear();
            MonoHelper.Instance.Clear();
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("SampleScene");
        });
        FlagData.Instance.Reward += ShowReward;
    }

    private void ShowReward(string show)
    {
        reward.text = show;
    }

    public override void OnSpawn(object obj)
    {
        string teamName = (string) obj;
        winTeamShow.color = FlagData.Instance.GetColor(teamName);
        winTeamShow.text = teamName +"获胜";
    }
}
