using Frame.Utility;
using Photon.Pun;
using UnityEngine;

namespace Game.flag.State
{
    public class OccupingState:BaseInteruptState
    {
        private float enterTime;
        private float occupyNeedTime = 5;
        private FlagHeroController heroController;
        public OccupingState(string stateName, FlagHeroController heroController) : base(stateName,heroController)
        {
            this.heroController = heroController;
            OnStateEnter += OnEnter;
            OnStateUpdate += OnUpdate;
            OnStateExit += OnExit;
            occupyNeedTime = ConfigurationManager.Instance.GetFlagConfig().occupyTime;
        }

        private void OnExit(Frame.FSM.State obj)
        {
            animator.SetBool("Loading",false);
        }

        private void OnUpdate(Frame.FSM.State obj)
        {
            if (!heroController.occuping)
            {
                return;
            }
            float duringTime = Time.time - enterTime;
            float progress = duringTime / occupyNeedTime;
            simpleHeroController.photonView.RPC("OnOccupyProgressChange",RpcTarget.All,new object[]{progress});
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            enterTime = Time.time;
            animator.SetBool("Loading",true);
        }
        
        protected override void IsMoveDo()
        {
            if(simpleHeroController is FlagHeroController flagController)
                flagController.occuping = false;
        }
        
    }
}