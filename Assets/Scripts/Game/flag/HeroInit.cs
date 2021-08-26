using Game.bean;
using Photon.Pun;
using UnityEngine;

public class HeroInit : MonoBehaviour
{
    private void Start()
    {
        //初始化自己的英雄
        string heroPath = PlayerInfo.Instance.GetChooseHeroPath();
        float x = Random.Range(-10f,-5f);
        float z = Random.Range(-10f,-5f);
        Vector3 p=new Vector3(x,1,z);
        PhotonNetwork.Instantiate(heroPath, p, Quaternion.identity,data:new object[]{0});
    }
}
