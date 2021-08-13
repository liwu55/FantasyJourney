using UnityEngine;

namespace Game.flag
{
    public class NiuNiuHeroController:FlagHeroController
    {
        
        //牛牛右键冲刺
        private float rushSpeed = 10;
        private float rotateSpeed = 180;
        
        protected override void AddSelfState()
        {
            base.AddSelfState();
            skill2State.OnStateUpdate += OnSkill2Update;
        }
        
        private void OnSkill2Update(Frame.FSM.State state)
        {
            velocity = transform.forward * rushSpeed;
            float h = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
        }
    }
}