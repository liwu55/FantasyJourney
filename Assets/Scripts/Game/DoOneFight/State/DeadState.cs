using UnityEngine;

namespace Game.DoOneFight.State
{
    public class DeadState: Frame.FSM.State
    {
        private PlayerCrtlr _playerCrtlr;
        private Animator animator;

        public DeadState(string stateName, PlayerCrtlr _playerCrtlr) : base(stateName)
        {
            this._playerCrtlr = _playerCrtlr;
            animator = _playerCrtlr.GetComponent<Animator>();
            _playerCrtlr._aniCtrler.Init(animator);
            OnStateEnter += OnEnter;
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            _playerCrtlr.cc.enabled = false;
            _playerCrtlr._aniCtrler.PlayAnimation((int) CharacterAniId.Death);
        }
    }
}