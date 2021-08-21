using System;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using Photon.Realtime;

namespace Game.flag
{
    /// <summary>
    /// 数据统计
    /// </summary>
    public class FlagShowData:SingleTonObj<FlagShowData>
    {
        private List<FlagDataInfo> infos;
        
        public void DamageSave(Player attacker,Player beAttacker,float damage,bool dead)
        {
            if (infos == null)
            {
                infos=new List<FlagDataInfo>();
            }

            FlagDataInfo infoAttack= GetInfo(attacker);
            infoAttack.damageTotal += damage;
            
            FlagDataInfo infoBeAttacker= GetInfo(beAttacker);
            infoBeAttacker.takeDamageTotal += damage;

            if (dead)
            {
                infoAttack.kill++;
                infoBeAttacker.dead++;
            }
            
            infos.Sort();
            //数据传递
            FlagData.Instance.photonView.RPC("OnDamageChange",
                RpcTarget.All,new object[]{GetDamageShow()});
        }

        private FlagDataInfo GetInfo(Player attacker)
        {
            int key = attacker.ActorNumber;
            for (int i = 0; i < infos.Count; i++)
            {
                if(infos[i].id==key)
                {
                    return infos[i];
                }
            }
            FlagDataInfo dataInfo = new FlagDataInfo();
            dataInfo.id = attacker.ActorNumber;
            dataInfo.name = attacker.NickName;
            dataInfo.team = new PhotonPlayerWrap(attacker).GetTeam();
            dataInfo.color = FlagData.Instance.GetColorStr(dataInfo.team);
            infos.Add(dataInfo);
            return dataInfo;
        }

        private class FlagDataInfo:IComparable<FlagDataInfo>
        {
            public int id;
            public string name;
            public float damageTotal;
            public float takeDamageTotal;
            public int kill;
            public int dead;
            public string team;
            public string color;
            public bool win = false;
            
            public int CompareTo(FlagDataInfo other)
            {
                if (other == null)
                {
                    return 1;
                }
                if (win != other.win)
                {
                    return win ? -1 : 1;
                }
                int killResult = kill.CompareTo(other.kill);
                if ( killResult!= 0)
                {
                    return -killResult;
                }

                int deadResult = dead.CompareTo(other.dead);
                if (deadResult != 0)
                {
                    return deadResult;
                }
                
                int damageTotalResult = damageTotal.CompareTo(other.damageTotal);
                if (damageTotalResult != 0)
                {
                    return -damageTotalResult;
                }
                
                return takeDamageTotal.CompareTo(other.takeDamageTotal);
            }
        }

        public string GetDamageShow()
        {
            StringBuilder sb=new StringBuilder();
            for (int i = 0; i < infos.Count; i++)
            {
                var info = infos[i];
                sb.Append(info.name);
                sb.Append("(");
                sb.Append(info.kill.ToString());
                sb.Append("杀");
                sb.Append(info.dead.ToString());
                sb.Append("死)：\n");
                sb.Append("\t造成伤害：");
                sb.Append(info.damageTotal);
                sb.Append("\n");
                sb.Append("\t承受伤害：");
                sb.Append(info.takeDamageTotal);
                sb.Append("\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="winTeam"></param>
        public void CalcReward(FlagData.TeamData winTeam)
        {
            if (infos == null)
            {
                return;
            }
            
            for (int i = 0; i < infos.Count; i++)
            {
                if (infos[i].team == winTeam.name)
                {
                    infos[i].win = true;
                }
            }
            infos.Sort();
            StringBuilder sb=new StringBuilder();
            StringBuilder honorDetail=new StringBuilder();
            StringBuilder moneyDetail=new StringBuilder();
            sb.Append("奖励\n");
            sb.Append("\n");
            for (int i = 0; i < infos.Count; i++)
            {
                honorDetail.Clear();
                moneyDetail.Clear();
                
                sb.Append("第");
                sb.Append((i + 1).ToString());
                sb.Append("：");
                sb.Append("<color=");
                sb.Append(infos[i].color);
                sb.Append(">");
                sb.Append(infos[i].name);
                sb.Append("</color>");
                sb.Append("\n");
                sb.Append("\t奖励荣誉：");
                int honor = GetHonor(i);
                
                honorDetail.Append("（第");
                honorDetail.Append((i + 1).ToString());
                honorDetail.Append("+");
                honorDetail.Append(honor);
                
                if (infos[i].win)
                {
                    honor += 1;
                    honorDetail.Append(" 获胜+1）");
                }
                else
                {
                    honorDetail.Append("）");
                }
                
                
                sb.Append(honor.ToString());
                sb.Append(honorDetail);
                sb.Append("\n");
                
                
                sb.Append("\t奖励金钱：");
                int money = GetMoney(i);
                
                moneyDetail.Append("（第");
                moneyDetail.Append((i + 1).ToString());
                moneyDetail.Append("+");
                moneyDetail.Append(money);
                
                if (infos[i].win)
                {
                    money += 1000;
                    moneyDetail.Append(" 获胜+1000）");
                }else
                {
                    moneyDetail.Append("）");
                }
                
                sb.Append(money.ToString());
                sb.Append(moneyDetail);
                
                sb.Append("\n");
                sb.Append("\n");
            }
            
            FlagData.Instance.photonView.RPC("OnReward",
                RpcTarget.All,new object[]{sb.ToString()});
        }

        private int GetMoney(int i)
        {
            switch (i)
            {
                case 0:
                    return 3000;
                case 1:
                    return 2000;
                case 2:
                    return 1000;
                case 3:
                case 4:
                    return 500;
                default:
                    return 300;
            }
        }

        private int GetHonor(int i)
        {
            switch (i)
            {
                case 0:
                    return 3;
                case 1:
                    return 2;
                case 2:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}