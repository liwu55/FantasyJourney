using System;
using System.Collections.Generic;
using Frame.Utility;
using Game.flag;
using Photon.Pun;
using UnityEngine;

public class MapInit : SingleTonMono<MapInit>
{
    private int rowCount = 3;
    private int columnCount = 3;
    private int flagDistance = 15;
    private Transform transPoints;

    protected override void Awake()
    {
        base.Awake();
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            return;    w
        }

        transPoints = GameObject.Find("Points").transform;
    }

    void Start()
    {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            return;
        }

        //仅房主实例化
        PointJudge.Instance.Init(rowCount);
        InitGreyPoints();
    }

    private void InitGreyPoints()
    {
        for (int i = 0; i < rowCount; i++)
        {
            int centerRow = rowCount / 2;
            float z = (i - centerRow) * flagDistance;
            for (int j = 0; j < columnCount; j++)
            {
                int centerColumn = columnCount / 2;
                float x = (j - centerColumn) * flagDistance;
                Vector3 position = new Vector3(x, 0, z);
                //网络实例化对象
                string greyPointPath = ConfigurationManager.Instance.GetPathByName("GreyPoint");
                GameObject greyPoint = PhotonNetwork.Instantiate(greyPointPath, position,
                    Quaternion.identity, 0, new object[] {i, j});
                greyPoint.transform.parent = transPoints;
            }
        }
    }

    public void ResetPoints(List<PointJudge.PointIndex> toResetPoints)
    {
        foreach (PointJudge.PointIndex toResetIndex in toResetPoints)
        {
            foreach (Transform t in transPoints)
            {
                PointBase point = t.GetComponent<PointBase>();
                if (point.rowIndex == toResetIndex.rowIndex && point.columnIndex == toResetIndex.columnIndex)
                {
                    point.Reset();
                    break;
                }
            }
        }
    }
}