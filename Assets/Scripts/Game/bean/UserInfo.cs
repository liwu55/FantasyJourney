using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.bean
{
    /// <summary>
    /// 用户信息，需要存储在数据库的信息
    /// </summary>
    public class UserInfo
    {
        
        public int id;
        public string username;
        public string password;
        //金币数
        public int money;
        //奖杯数
        public int honor;
        //已经拥有的英雄
        public List<int> ownedHeroes;

        public string ownedHero;
        
        public int isLogined;
      

        public UserInfo(int id, string username, string password, int money, int honor, string ownedHero,int isLogined)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.money = money;
            this.honor = honor;
            this.ownedHero = ownedHero;
            try
            {
                string[] heroesStr = ownedHero.Split(',');
                ownedHeroes = new List<int>();
                for (int i = 0; i < heroesStr.Length; i++)
                {
                    ownedHeroes.Add(Int32.Parse(heroesStr[i]));
                }
            }
            catch (Exception e)
            {
            }

            this.isLogined = isLogined;
            
        }

        public bool CheckIfHas(int heroId)
        {
            if (ownedHeroes == null)
            {
                return false;
            }

            for (int i = 0; i < ownedHeroes.Count; i++)
            {
                if (heroId == ownedHeroes[i])
                {
                    return true;
                }
            }
            return false;
        }

        public void BuyHero(int heroId)
        {
            ownedHero += ",";
            ownedHero += heroId.ToString();
            ownedHeroes.Add(heroId);
            DataBaseManager.Instance.SaveInfo(this);
        }
    }
}