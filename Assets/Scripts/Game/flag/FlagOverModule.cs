using Frame.UI;
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
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Menu");
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
