using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*private float speed = 5f;
    PlayerAnim playerAnim;
    private Animator _animator;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        playerAnim = new PlayerAnim();
        _animator = GetComponent<Animator>();
        playerAnim.Init(_animator);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0)
        {
            playerAnim.PlayRun();
            transform.Translate(new Vector3(0,0 ,h*speed * Time.deltaTime));
        }
        else
        {
            playerAnim.PlayIdle();
        }
    }*/
}
