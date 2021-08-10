using Frame.FSM;
using UnityEngine;

namespace Game.DoOneFight.State
{
    public class PlayerCrtlr : MonoBehaviour
{
    private CharacterController cc;
    private Animator _animator;
    private CharacterAniCtrler _aniCtrler;
    private float speed = 5f;
    private float smooth = 2f;
    private bool isNormalAttack;
    private bool isSkillAttack;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aniCtrler = gameObject.GetComponent<CharacterAniCtrler>();
        cc = transform.GetComponent<CharacterController>();
        _aniCtrler.Init(_animator);
        //非战斗总状态机
        StateMachine playerStateMachine = new StateMachine("playerStateMachine");
        Frame.FSM.State normalState = new NormalState("Normal",this);
        //状态
        playerStateMachine.AddState(normalState);
        //攻击总状态机
        StateMachine attackStateMachine = new StateMachine("attackStateMachine");
        attackStateMachine.AddState("Attack");
        //攻击状态机下的字子状态机
        StateMachine normalAtkStateMachine = new StateMachine("NormalAttack");
        StateMachine skillAtkStateMachine = new StateMachine("SkillAttack");
        //攻击状态机添加子状态(普通攻击)·
        attackStateMachine.AddState(normalAtkStateMachine);
        attackStateMachine.AddState(skillAtkStateMachine);
    }
    void Start()
    {
        InputMgr.Instance.InitInputDic();
    }

    //计算夹角的角度 0~360
    /*private float GetAngle(Vector3 from_, Vector3 to_)
    {
        Vector3 v3 = Vector3.Cross(from_, to_);
        if (v3.z >= 0)
            return Vector3.Angle(from_, to_);
        else
            return 360 - Vector3.Angle(from_, to_);
    }*/

    void FixedUpdate()
    {
        //Move();
        //Action();
        
    }

    /*void Move()
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
            _aniCtrler.PlayRun();
        }
        else
        {
            _aniCtrler.PlayIdle();
        }
        Vector3 dirWorld = Camera.main.transform.TransformDirection(dir);
        dirWorld.y = 0;
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
    */


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
}
