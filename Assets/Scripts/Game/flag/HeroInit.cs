using System.Diagnostics;
using Frame.Utility;
using Photon.Pun;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class HeroInit : MonoBehaviour
{
    private void Start()
    {
        //初始化自己的英雄
        //根据自己选择的英雄来实例化,先固定为牛牛
        int value = Random.Range(0, 3);
        Debug.Log("value="+value);
        string heroPath = "";
        switch (value)
        {
            case 0: 
                heroPath = ConfigurationManager.Instance.GetPathByName("Skull");
                break;
            case 1: 
                heroPath = ConfigurationManager.Instance.GetPathByName("niuniu");
                break;
            case 2: 
                heroPath = ConfigurationManager.Instance.GetPathByName("cat");
                break;
        }
        heroPath = ConfigurationManager.Instance.GetPathByName("cat");
        float x = Random.Range(-10f,10f);
        float z = Random.Range(-10f,10f);
        Vector3 p=new Vector3(x,1,z);
        PhotonNetwork.Instantiate(heroPath, p, Quaternion.identity);
    }
}
