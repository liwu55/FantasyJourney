
namespace Game.DoOneFight.State
{
    public class AttackState : Frame.FSM.State
    {
        private PlayerCrtlr _playerCrtlr;
        public AttackState(string stateName,PlayerCrtlr _playerCrtlr) : base(stateName)
        {
            this._playerCrtlr = _playerCrtlr;
        }
    }
}

