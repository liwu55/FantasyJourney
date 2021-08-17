using UnityEngine;
namespace Game.DoOneFight.State
{
    public class Skill_01_AttackState : Frame.FSM.State
    {
        private PlayerCrtlr _playerCrtlr;
        private Animator animator;
        float checkFixTime = 2f;
        
        public Skill_01_AttackState(string stateName,PlayerCrtlr _playerCrtlr) : base(stateName)
        {
            this._playerCrtlr = _playerCrtlr;
            animator = _playerCrtlr.GetComponent<Animator>();
            _playerCrtlr._aniCtrler.Init(animator);
            OnStateEnter += OnEnter;
            OnStateUpdate += OnUpdate;
            OnStateExit += OnExit;
        }
        private void OnUpdate(Frame.FSM.State obj)
        {
            if (_playerCrtlr.isOnSkill_01)
            {
                checkFixTime -= Time.deltaTime;
                if (checkFixTime<=0)
                {
                    if (_playerCrtlr.isOnSkill_01)
                    {
                        _playerCrtlr.isOnSkill_01 = false;
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
            _playerCrtlr.cc.enabled = false;
            _playerCrtlr._aniCtrler.PlayAnimation((int) CharacterAniId.HeavyAttack);
        }
        void OnExit(Frame.FSM.State obj)
        {
            _playerCrtlr._aniCtrler.RestAction();
        }
    }
}

