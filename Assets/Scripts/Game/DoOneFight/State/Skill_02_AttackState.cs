using UnityEngine;
namespace Game.DoOneFight.State
{
    public class Skill_02_AttackState : Frame.FSM.State
    {
        private PlayerCrtlr _playerCrtlr;
        private Animator animator;
        private float checkFixTime = 2f;
        
        public Skill_02_AttackState(string stateName,PlayerCrtlr _playerCrtlr) : base(stateName)
        {
            this._playerCrtlr = _playerCrtlr;
            animator = _playerCrtlr.GetComponent<Animator>();
            _playerCrtlr._aniCtrler.Init(animator);
            OnStateEnter += OnEnter;
            OnStateExit += OnExit;
            OnStateUpdate += OnUpdate;
        }

        
        private void OnUpdate(Frame.FSM.State obj)
        {
            if (_playerCrtlr.isOnSkill_02)
            {
                Debug.Log("OnUpdate Skill2");
                checkFixTime -= Time.deltaTime;
                if (checkFixTime<=0)
                {
                    if (_playerCrtlr.isOnSkill_02)
                    {
                        _playerCrtlr.isOnSkill_02 = false;
                        checkFixTime = 2f;
                    }
                    else
                    {
                        checkFixTime = 2f;
                    }
                }
            }
        }
        
        //进入该状态时播放动作
        private void OnEnter(Frame.FSM.State obj)
        {
            Debug.Log("Skill_02_AttackState OnEnter");
            _playerCrtlr.cc.enabled = false;
            _playerCrtlr._aniCtrler.PlayAnimation((int) CharacterAniId.Skill_2);
        }
        void OnExit(Frame.FSM.State obj)
        {
            _playerCrtlr.cc.enabled = true;
        }
    }
}