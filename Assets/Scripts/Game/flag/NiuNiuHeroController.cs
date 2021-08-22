using UnityEngine;

namespace Game.flag
{
    public class NiuNiuHeroExtra
    {
        private SimpleHeroController simpleHeroController;
        //牛牛右键冲刺
        private float rushSpeed = 10;
        private float rotateSpeed = 180;
        
        public void OnSkill2Update(Frame.FSM.State state)
        {
            simpleHeroController.velocity = simpleHeroController.transform.forward * rushSpeed;
            float h = Input.GetAxis("Horizontal");
            simpleHeroController.transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
        }

        public void SetController(SimpleHeroController simpleHeroController)
        {
            this.simpleHeroController = simpleHeroController;
        }
    }
}