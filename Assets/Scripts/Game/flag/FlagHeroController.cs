using System;
using ExitGames.Client.Photon;
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
    
        //牛牛右键冲刺
        private float rushSpeed = 10;
        private float rotateSpeed = 180;

        protected override void Awake()
        {
            base.Awake();
            heroUI = transform.Find("HeroUI").GetComponent<HeroUI>();
        }

        private void Start()
        {
            heroUI.SetHeroName(photonView.Owner.NickName);
        }

        protected override void AddSelfState()
        {
            Frame.FSM.State occupingState = new OccupingState("Occuping", this);
            heroStateMachine.AddState(occupingState);
            occupingState.AddTransition("Normal", () => !occuping);
            normalState.AddTransition("Occuping", () => occuping);

            normalState.OnMouseLeftClick += CheckClickPoint;
            skill2State.OnStateUpdate += OnSkill2Update;
        }

        public string GetPointSign()
        {
            return new PhotonPlayerWrap(photonView.Owner).GetTeam();
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
                    string teamName = GetPointSign();
                    string pointSign = FlagData.Instance.GetPointSign(teamName);
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
        
        private void OnSkill2Update(Frame.FSM.State state)
        {
            velocity = transform.forward * rushSpeed;
            float h = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
        }
    }
}