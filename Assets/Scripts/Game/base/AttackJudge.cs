using UnityEngine;

namespace Game
{
    /// <summary>
    /// 攻击判定
    /// </summary>
    public static class AttackJudge
    {
        /// <summary>
        /// 圆形判定
        /// </summary>
        /// <param name="self"></param>
        /// <param name="enemy"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool CircleAttack(Transform self,Transform enemy,float range)
        {
            Vector3 i2Target = enemy.transform.position - self.position;
            //超出攻击范围
            if (i2Target.magnitude > range)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 扇形判定
        /// </summary>
        /// <returns></returns>
        public static bool SectorAttack(Transform self,Transform enemy,float range,float angle)
        {
            Vector3 i2Target = enemy.transform.position - self.position;
            //超出攻击范围
            if (i2Target.magnitude > range)
            {
                return false;
            }

            Vector3 forward = self.forward;
            i2Target.Normalize();
            float cosValue = Vector3.Dot(forward, i2Target);
            float angleI2Emeny = Mathf.Rad2Deg * Mathf.Acos(cosValue);
            //超出角度
            if (Mathf.Abs(angleI2Emeny) > angle / 2)
            {
                return false;
            }
            return true;
        }
    }
}