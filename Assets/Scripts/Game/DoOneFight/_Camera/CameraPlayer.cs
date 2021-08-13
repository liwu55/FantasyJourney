using System.Collections.Generic;
using UnityEngine;

namespace Game.DoOneFight._Camera
{
    public class CameraPlayer : MonoBehaviour
    {
        public const float DISTANCE_DEAFULT = 8.0f; //默认主角和摄像机的距离
        private float distance = 0.0f; //运行时的距离
        private Transform transTarget; //摄像机跟随目标（主角）

        public float target_offsety = 0.5f; //盯着目标往上偏移值
        private Vector3 vLooker; //摄像机盯着的位置

        private Vector3 rotateXY; //保存X,Y方向的旋转，鼠标移动的时候会发生改变（跟鼠标移动保持一致性，便于理解）
        private float speedAngelX = 200; //x方向的旋转速度
        private float speedAngelY = 100; //y方向的旋转速度
        public float minAngleAtY = -90.0f; //摄像机在Y方向的角度的最小值
        public float maxAngleAtY = 90.0f; //摄像机在Y方向的角度的最大值
        Quaternion rotation; //摄像机的旋转角度

        void Start()
        {
            distance = DISTANCE_DEAFULT;
            transTarget = GameObject.FindGameObjectWithTag("Player").transform;
            vLooker = new Vector3(transTarget.position.x, transTarget.position.y + target_offsety,
                transTarget.position.z);
            rotation = transform.rotation; //摄像机当前旋转角度

            rotateXY.y = transform.eulerAngles.x;
            rotateXY.x = transform.eulerAngles.y;

            transform.position = transTarget.position + rotation * Vector3.back * distance;
            transform.LookAt(vLooker); //一直看着某个点
        }

        void LateUpdate() //射线的碰撞检测
        {
            if (transTarget)
            {
                vLooker = transTarget.position;
                vLooker.y += target_offsety;
                transform.position = transTarget.position + rotation * Vector3.back * distance;
                //按下鼠标右键
              

                //射线和场景中的模型产生的所有碰撞点
                RaycastHit[] hits = Physics.RaycastAll(new Ray(vLooker, (transform.position - vLooker).normalized));
                distance = DISTANCE_DEAFULT;
                List<RaycastHit> listHits = new List<RaycastHit>(hits);
                foreach (RaycastHit hit in listHits)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        listHits.Remove(hit);
                        //break;
                    }
                }

                if (listHits.Count > 0)
                {
                    RaycastHit stand = listHits[0]; //剔除人物身上点之后，距离人最近的点
                    foreach (RaycastHit hit in listHits)
                    {
                        //if (hit.collider.gameObject.tag == "terrain")
                        //{
                        //    if (hit.distance < stand.distance)
                        //    {
                        //        stand = hit;
                        //    }
                        //}
                        if (hit.distance < stand.distance)
                        {
                            stand = hit;
                        }
                    }

                    Debug.Log(stand.point + " " + stand.collider.gameObject.tag);
                    string tag = stand.collider.gameObject.tag;
                    distance = Vector3.Distance(stand.point, vLooker);
                    if (distance > DISTANCE_DEAFULT)
                    {
                        distance = DISTANCE_DEAFULT;
                    }

                }

                print("vLooker=" + vLooker);
                Debug.DrawRay(vLooker, transform.position - vLooker, Color.red);
                Vector3 position = transTarget.position + rotation * Vector3.back * distance;
                position.y += 1.5f; //为了不让摄像机看到地下的东西
                transform.position = Vector3.Lerp(transform.position, position, 0.3f);

                transform.LookAt(vLooker);

            }

        }
    }
}