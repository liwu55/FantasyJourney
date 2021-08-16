using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BFF_Rules : MonoBehaviour
{

    public int Score = 0;
    public int Hp = 100;

   
    private void OnTriggerEnter(Collider other)
    {
        //徽章
        if (other.CompareTag("Badges"))
        {
            Score += 1;
            other.gameObject.SetActive(false);
        }
        //骰子
        else if (other.CompareTag("Dice"))
        {
            Score += Random.Range(1, 7);
            other.gameObject.SetActive(false);
        }
        //礼物盒
        else if (other.CompareTag("Gift"))
        {
            Score += 20;
            other.gameObject.SetActive(false);
        }
        //面包
        else if (other.CompareTag("Bread"))
        {
            Hp += 10;
            other.gameObject.SetActive(false);
        }
        //汉堡
        else if (other.CompareTag("Burger"))
        {
            Hp += 30;
            other.gameObject.SetActive(false);
        }
        //酒
        else if (other.CompareTag("Wine"))
        {
            
        }
        
    }
    
}
