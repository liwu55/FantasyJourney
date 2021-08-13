using System;
using ExitGames.Client.Photon;
using Frame.Utility;
using Game.flag.State;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game.flag
{
    public class FlagHeroController : SimpleHeroController
    {
        [NonSerialized] public bool occuping = false;

        //插旗距离
        public float operationDistance = 3;

        //正在占领的据点
        [NonSerialized] public PointBase occupingPoint;

        //进度条
        private HeroUI heroUI;


        private Vector3 hitBackVelocity;

        protected override void Awake()
        {
            base.Awake();
            heroUI = transform.Find("HeroUI").GetComponent<HeroUI>();
        }

        private void Start()
        {
            //不是网络情况下就直接返回
            if (photonView.Owner == null)
            {
                return;
            }
            heroUI.gameObject.SetActive(true);
            heroUI.SetHeroName(photonView.Owner.NickName);
        }

        protected override void FixedUpdate()
        {
            if (!photonView.IsMine)
            {
                return;
            }
            base.FixedUpdate();
            //被击退
            if(hitBackVelocity.magnitude>0.1f){
                cc.SimpleMove(hitBackVelocity);
                hitBackVelocity = Vector3.Lerp(hitBackVelocity, Vector3.zero, Time.deltaTime * 6);
            }
        }

        protected override void AddSelfState()
        {
            Frame.FSM.State occupingState = new OccupingState("Occuping", this);
            heroStateMachine.AddState(occupingState);
            occupingState.AddTransition("Normal", () => !occuping);
            normalState.AddTransition("Occuping", () => occuping);

            normalState.OnMouseLeftClick += CheckClickPoint;
            
        }

        public string GetPointSign()
        {
            string teamName = new PhotonPlayerWrap(photonView.Owner).GetTeam();
            return FlagData.Instance.GetPointSign(teamName);
        }

        //占领的进度改变了
        [PunRPC]
        public void OnOccupyProgressChange(float progress)
        {
            if (!heroUI.IsShowLoading())
            {
                heroUI.ShowLoading();
            }

            heroUI.ShowProgress(progress);
            if (progress >= 1)
            {
                heroUI.HideLoading();
                occuping = false;
                //房主同步一下
                if (PhotonNetwork.IsMasterClient)
                {
                    string pointSign = GetPointSign();
                    if (pointSign != null)
                    {
                        occupingPoint.photonView.RPC("ChangeOccupiedSign",
                            RpcTarget.MasterClient, new object[] {pointSign});
                    }
                }
            }
        }

        [PunRPC]
        public void SetOccupingPoint(int viewId)
        {
            this.occupingPoint = PhotonNetwork.GetPhotonView(viewId).GetComponent<PointBase>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (targetPlayer == photonView.Owner)
            {
                if (changedProps.ContainsKey(PhotonPlayerWrap.TEAM))
                {
                    Color color = FlagData.Instance.GetColor((string) changedProps[PhotonPlayerWrap.TEAM]);
                    heroUI.SetNameColor(color);
                }
            }
        }

        private bool CheckClickPoint()
        {
            PointBase occupingPoint;
            if (CheckIfClickPoint(out occupingPoint))
            {
                photonView.RPC("SetOccupingPoint",
                    RpcTarget.MasterClient, new object[] {occupingPoint.photonView.ViewID});
                //转向该点
                transform.forward =
                    occupingPoint.transform.position -
                    transform.position;
                occuping = true;
                return true;
            }

            return false;
        }

        private bool CheckIfClickPoint(out PointBase occupingPoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int flayPointLayer = LayerMask.NameToLayer("flagPoint");
            bool hitFlag = Physics.Raycast(ray, out hit, 1000, 1 << flayPointLayer);
            if (hitFlag)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                Debug.Log("distance=" + distance);
                if (distance > operationDistance)
                {
                    Debug.Log("距离太远，不能操作");
                }
                else
                {
                    Debug.Log("距离在范围内，可以操作");
                    PointBase point = hit.collider.GetComponent<PointBase>();
                    if (!point.IsOccupied() || point.GetOccupiedSign() != GetPointSign())
                    {
                        Debug.Log("占领该据点");
                        occupingPoint = point;
                        return true;
                    }
                    else
                    {
                        Debug.Log("该据点已占领");
                    }
                }
            }

            occupingPoint = null;
            return false;
        }

        [PunRPC]
        public override void BeAttack(Vector3 point,Vector3 dir,string effectName, float damage,float hitBackFactor = 1)
        {
            base.BeAttack(point, dir,effectName,damage,hitBackFactor);
            ShowHitEffect(effectName,point,dir);
            SyncLifeShow();
            //被攻击打断插旗动作
            occuping = false;
            heroUI.HideLoading();
            
            //击退，根据攻击力产生击退力
            Vector3 hitBackDir = -dir;
            hitBackDir.y = 0;
            hitBackDir.Normalize();
            hitBackVelocity += hitBackDir * damage * 0.3f * hitBackFactor;
        }

        private void ShowHitEffect(string effectName,Vector3 point,Vector3 dir)
        {
            GameObject go = ObjectPool.Instance.SpawnObj(effectName);
            go.transform.position = point;
            go.transform.forward = dir;
            Destroy(go,2);
        }

        private void SyncLifeShow()
        {
            float lifePercent = life / 100f;
            heroUI.SetLife(lifePercent);
        }

        public override void ResetState()
        {
            base.ResetState();
            photonView.RPC("ResetRPC", RpcTarget.All);
        }

        [PunRPC]
        public void ResetRPC()
        {
            life = 100;
            SyncLifeShow();
        }
    }
}