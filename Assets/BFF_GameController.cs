using Frame.Utility;
using Photon.Pun;
using UnityEngine;

public class BFF_GameController : MonoBehaviour
{

    void Start()
    {
        //初始化自己的英雄
        //根据自己选择的英雄来实例化,先固定为英雄
        string heroPath = ConfigurationManager.Instance.GetPathByName("Skull");
        //float x = Random.Range(-10f,10f);
        float z = Random.Range(-10.0f,0.0f);
        Vector3 p=new Vector3(0,0,z);
        PhotonNetwork.Instantiate(heroPath, p, Quaternion.identity);
    }

   
}
