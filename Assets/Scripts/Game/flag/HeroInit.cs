using Frame.Utility;
using Photon.Pun;
using UnityEngine;

public class HeroInit : MonoBehaviour
{
    private void Start()
    {
        //初始化自己的英雄
        //根据自己选择的英雄来实例化,先固定为牛牛
        string heroPath = ConfigurationManager.Instance.GetPathByName("niuniu");
        float x = Random.Range(-10f,10f);
        float z = Random.Range(-10f,10f);
        Vector3 p=new Vector3(x,1,z);
        PhotonNetwork.Instantiate(heroPath, p, Quaternion.identity);
    }
}
