using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
   private Transform[] PlayersTrans;
   private GameObject[] PlayersGo;
   private float xMin, xMax, yMin, yMax;
   private float yOffset = 2.5f;
   private float minDistance = 12f;
   private void Awake()
   {
      PlayersGo = GameObject.FindGameObjectsWithTag("Player");
      PlayersTrans = new Transform[PlayersGo.Length];
   }

   private void Start()
   {
      for (int i = 0; i < PlayersGo.Length; i++)
      {
         PlayersTrans[i] = PlayersGo[i].transform;
      }
   }

   private void LateUpdate()
   {
      xMin = xMax = PlayersTrans[0].position.x;
      yMin = yMax = PlayersTrans[0].position.y;
      for (int i = 1; i < PlayersTrans.Length; i++)
      {
         if (PlayersTrans[i].position.x<xMin)
            xMin = PlayersTrans[i].position.x;
         if (PlayersTrans[i].position.x>xMax)
            xMax = PlayersTrans[i].position.x;
         if (PlayersTrans[i].position.y<yMin)
            yMin = PlayersTrans[i].position.y;
         if (PlayersTrans[i].position.y>yMax)
            yMax = PlayersTrans[i].position.y;
      }
      float xmidDistance = (xMax + xMin) / 2;
      float ymidDistance = (yMax + yMin) / 2;
      float distance = xMax - xMin;
      if (distance<minDistance)
      {
         distance = minDistance;
      }
      transform.position = new Vector3(xmidDistance, ymidDistance + yOffset,-distance);
   }
}
   
   