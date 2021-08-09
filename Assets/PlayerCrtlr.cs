using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCrtlr : MonoBehaviour
{
    private CharacterController cc;
    private Animator _animator;
    private CharacterAniCtrler _aniCtrler;
    private float speed = 5f;
    private float smooth = 2f;
    Vector3 lastLook;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _aniCtrler = gameObject.GetComponent<CharacterAniCtrler>();
        cc = transform.GetComponent<CharacterController>();
        _aniCtrler.Init(_animator);
        InputMgr.Instance.InitInputDic();
    }

    //计算夹角的角度 0~360
    float GetAngle(Vector3 from_, Vector3 to_)
    {
        Vector3 v3 = Vector3.Cross(from_, to_);
        if (v3.z >= 0)
            return Vector3.Angle(from_, to_);
        else
            return 360 - Vector3.Angle(from_, to_);
    }

    void FixedUpdate()
    {
        Move();
        //Action();
        
    }

    void Move()
    {
        if (Input.GetKeyDown(InputMgr.Instance.inputDic[EKeyName.jump]))
        {
            _aniCtrler.PlayAnimation((int)CharacterAniId.Jump);
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        if (dir.magnitude > 0)
        {
            _aniCtrler.IsPlayRunAction = true;
        }
        else
        {
            _aniCtrler.IsPlayRunAction = false;
            _aniCtrler.PlayIdle();
        }
        Vector3 dirWorld = Camera.main.transform.TransformDirection(dir);
        dirWorld.y = 0;
        print(GetAngle(transform.forward, dirWorld));
        if (GetAngle(transform.forward, dirWorld) > 135f)
        {
            transform.forward = dirWorld;
        }
        else
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 10f);
        }
        cc.SimpleMove(speed * dir);
    }


    void Action()
    {
        if (Input.GetKeyDown(InputMgr.Instance.inputDic[EKeyName.normalAttack]))
        {
            //播放动画
            _aniCtrler.PlayAnimation((int)CharacterAniId.NormalAttack);
        }
        //跳跃
       
    }
}