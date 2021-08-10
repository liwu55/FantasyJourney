using UnityEngine;


namespace Game.DoOneFight.State
{
    public class NormalState : Frame.FSM.State
    {
        private PlayerCrtlr _playerCrtlr;
        private CharacterController cc;
        private Animator _animator;
        private CharacterAniCtrler _aniCtrler;
        private float speed = 5f;
        private float smooth = 2f;
        private bool isNormalAttack;
        private bool isSkillAttack;
        public NormalState(string stateName,PlayerCrtlr _playerCrtlr) : base(stateName)
        {
            this._playerCrtlr = _playerCrtlr;
            _animator = _playerCrtlr.GetComponent<Animator>();
            _aniCtrler = _playerCrtlr.GetComponent<CharacterAniCtrler>();
            cc = _playerCrtlr.GetComponent<CharacterController>();
            
            OnStateUpdate += OnUpdate;
            OnStateExit += OnExit;
            OnStateEnter += OnEnter;
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            
        }
        private void OnUpdate(Frame.FSM.State obj)
        {
            Move();
        }
        private void OnExit(Frame.FSM.State obj)
        {
            
        }
        
        
        private float GetAngle(Vector3 from_, Vector3 to_)
        {
            Vector3 v3 = Vector3.Cross(from_, to_);
            if (v3.z >= 0)
                return Vector3.Angle(from_, to_);
            else
                return 360 - Vector3.Angle(from_, to_);
        }
        private void Move()
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
            if (GetAngle(_playerCrtlr.transform.forward, dirWorld) > 135f)
            {
                _playerCrtlr.transform.forward = dirWorld;
            }
            else
            {
                _playerCrtlr.transform.forward = Vector3.Lerp(_playerCrtlr.transform.forward, dir, Time.deltaTime * 10f);
            }
            cc.SimpleMove(speed * dir);
        }
    }

   
}