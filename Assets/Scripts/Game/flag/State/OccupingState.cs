using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game.flag.State
{
    public class OccupingState:Frame.FSM.State
    {
        private SimpleHeroController simpleHeroController;
        private Animator animator;
        private float enterTime;
        //TODO 占领需要的时间
        private float occupyNeedTime = 2;
        public OccupingState(string stateName, SimpleHeroController simpleHeroController) : base(stateName)
        {
            this.simpleHeroController = simpleHeroController;
            animator = simpleHeroController.GetComponentInChildren<Animator>();
            OnStateEnter += OnEnter;
            OnStateUpdate += OnUpdate;
            OnStateExit += OnExit;
        }

        private void OnExit(Frame.FSM.State obj)
        {
            animator.SetBool("Loading",false);
        }

        private void OnUpdate(Frame.FSM.State obj)
        {
            float duringTime = Time.time - enterTime;
            float progress = duringTime / occupyNeedTime;
            simpleHeroController.photonView.RPC("OnOccupyProgressChange",RpcTarget.All,new object[]{progress});
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            enterTime = Time.time;
            animator.SetBool("Loading",true);
        }
        
    }
}