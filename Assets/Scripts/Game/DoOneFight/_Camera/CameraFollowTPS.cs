using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTPS : SingleTonMono<CameraFollowTPS>
{
    private Transform _transform;
    private Vector3 _offset;
    private Transform PlayerTrans;
    private Vector3 targetPos;
    
    //float 
    float speed = 10.0F;

    protected override void Awake()
    {  
        base.Awake();
        _transform = transform;
    }

    void InitCameraPos()
    {
        _transform.position = PlayerTrans.position + Vector3.back * 7.5f +Vector3.up * 8f;
    }


    public void BindPlayer(Transform _playerTrans)
    {
        PlayerTrans = _playerTrans;
        InitCameraPos();
        _offset = _transform.position - PlayerTrans.position;
       
    }
    void Update()
    {
        if (PlayerTrans!=null && _offset!=null)
        {
            targetPos = PlayerTrans.position + _offset;
            transform.position=Vector3.Lerp(gameObject.transform.position,targetPos,Time.deltaTime*speed);
            
        }
        //transform.LookAt(targetPos);
    }
   
}
