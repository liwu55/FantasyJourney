using System.Collections;
using Frame.Utility;
using Game.bean;
using Photon.Pun;
using UnityEngine;

public class BFF_GameController : MonoBehaviour
{

    void Start()
    {
        //初始化自己的英雄
        //根据自己选择的英雄来实例化,先固定为英雄
        string heroPath = PlayerInfo.Instance.GetChooseHeroPath();
        //float x = Random.Range(-10f,10f);
        float z = Random.Range(-10.0f,0.0f);
        Vector3 p=new Vector3(0,0,z);
        PhotonNetwork.Instantiate(heroPath, p, Quaternion.identity);
    }
    
    
    //每种物品有自己的点
    //每隔一分钟刷新一次
    //物品最多存在50s
    //或许物品的点 然后随机生成


   




}
