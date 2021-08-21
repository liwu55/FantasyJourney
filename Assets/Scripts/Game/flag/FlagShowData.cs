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
            
            public int CompareTo(FlagDataInfo other)
            {
                if (other == null)
                {
                    return 1;
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
    }
}