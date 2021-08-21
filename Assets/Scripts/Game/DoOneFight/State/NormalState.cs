using Photon.Pun;
using UnityEngine;


namespace Game.DoOneFight.State
{
    public class NormalState : Frame.FSM.State
    {
        private PlayerCrtlr _playerCrtlr;
        private Animator _animator;
        private CharacterAniCtrler _aniCtrler;
        private float smooth = 2f;
        private bool isSkillAttack;
        private float jumpForce = 0.1f;
        public NormalState(string stateName,PlayerCrtlr _playerCrtlr) : base(stateName)
        {
            this._playerCrtlr = _playerCrtlr;
            _animator = _playerCrtlr.transform.GetComponent<Animator>();
            _animator.SetLayerWeight(1,1);
            _playerCrtlr._aniCtrler.Init(_animator);
            OnStateUpdate += OnUpdate;
            OnStateEnter += OnEnter;
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            Debug.Log("NormalState OnEnter");
        }
        private void OnUpdate(Frame.FSM.State obj)
        {
            Move();
            CheckIsNrmAtk();
            _playerCrtlr.isOnSkill_01 = ComboSystem.Instance.CheckSkill01();
            _playerCrtlr.isOnSkill_02 = ComboSystem.Instance.CheckSkill02();
        }

        private void ResetAction()
        {
            _playerCrtlr._aniCtrler.RestAction();
        }
        /// <summary>
        /// 人物移动
        /// </summary>
        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            _playerCrtlr.dir = new Vector3(h, 0, v);
            if (Input.GetKeyDown(InputMgr.Instance.inputDic[EKeyName.jump]))
            {
                _playerCrtlr.dir.y = jumpForce;
                _playerCrtlr._aniCtrler.PlayAnimation((int)CharacterAniId.Jump);
            }
            if ( _playerCrtlr.dir.magnitude > 0)
            {
                _playerCrtlr._aniCtrler.PlayRun();
            }
            else
            {
                _playerCrtlr._aniCtrler.PlayIdle();
            }
            
            Vector3 dirWorld = Camera.main.transform.TransformDirection(_playerCrtlr.dir);
            dirWorld.y = 0;
            if (GetAngle(_playerCrtlr.transform.forward, dirWorld) > 135f)
            {
                _playerCrtlr.transform.forward = dirWorld;
            }
            else
            {
                _playerCrtlr.transform.forward = Vector3.Lerp(_playerCrtlr.transform.forward,  _playerCrtlr.dir, Time.deltaTime * 10f);
            }
        }

        /// <summary>
        /// Update中判断是否进行普通攻击
        /// </summary>
        private void CheckIsNrmAtk()
        {
            if (Input.GetKeyDown(InputMgr.Instance.inputDic[EKeyName.normalAttack]))
            {
                _playerCrtlr.isNrmAtk = true;
                //_playerCrtlr._aniCtrler.PlayAnimation((int)CharacterAniId.NormalAttack);                
            }
        }
        /// <summary>
        /// 获得旋转角度
        /// </summary>
        /// <param name="from_"></param>
        /// <param name="to_"></param>
        /// <returns></returns>
        private float GetAngle(Vector3 from_, Vector3 to_)
        {
            Vector3 v3 = Vector3.Cross(from_, to_);
            if (v3.z >= 0)
                return Vector3.Angle(from_, to_);
            else
                return 360 - Vector3.Angle(from_, to_);
        }
    }
}