using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTPS : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _offset;
    private Transform PlayerTrans;
    private Vector3 targetPos;
    float speed = 5.0F;
    private void Awake()
    {  
        _transform = this.transform;
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        _offset = _transform.position - PlayerTrans.position;
    }


  
    void Update()
    {
        targetPos = PlayerTrans.position + _offset;
        transform.position=Vector3.Lerp(gameObject.transform.position,targetPos,Time.deltaTime*speed);
        //transform.LookAt(targetPos);
    }
   
}
