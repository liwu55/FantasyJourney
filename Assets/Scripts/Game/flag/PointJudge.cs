using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Game.flag
{
    public class PointJudge : SingleTonObj<PointJudge>
    {
        private string[,] pointSigns;
        private string unoccupiedSign;
        private int dimensionCount;

        public void Init(int dimensionCount)
        {
            unoccupiedSign = FlagData.Instance.GetUnoccupiedSignName();
            this.dimensionCount = dimensionCount;
            pointSigns = new string[dimensionCount, dimensionCount];
            for (int i = 0; i < dimensionCount; i++)
            {
                for (int j = 0; j < dimensionCount; j++)
                {
                    pointSigns[i, j] = unoccupiedSign;
                }
            }
        }

        public void Reset()
        {
            for (int i = 0; i < dimensionCount; i++)
            {
                for (int j = 0; j < dimensionCount; j++)
                {
                    pointSigns[i, j] = unoccupiedSign;
                }
            }
        }

        public void Change(int rowIndex, int columnIndex, string sign)
        {
            pointSigns[rowIndex, columnIndex] = sign;
            //等一会再判断
            MapInit.Instance.StartCoroutine(DelayJudge());
        }
        
        public void JustChange(int rowIndex, int columnIndex, string sign)
        {
            pointSigns[rowIndex, columnIndex] = sign;
        }

        IEnumerator DelayJudge()
        {
            yield return new WaitForSeconds(0.6f);
            Judge();
        }

        /// <summary>
        /// 判断是否连成一排
        /// </summary>
        public void Judge()
        {
            List<PointIndex> toResetPoints = new List<PointIndex>();
            string winSign = "";
            int sucCount = 0;
            //横竖排
            for (int i = 0; i < dimensionCount; i++)
            {
                string firstColumnSign = pointSigns[i, 0];
                bool rowAllSame = false;
                if (firstColumnSign != unoccupiedSign)
                {
                    rowAllSame = true;
                    for (int j = 1; j < dimensionCount; j++)
                    {
                        if (pointSigns[i, j] != firstColumnSign)
                        {
                            rowAllSame = false;
                            break;
                        }
                    }
                }

                string firstRowSign = pointSigns[0, i];
                bool columnAllSame = false;
                if (firstRowSign != unoccupiedSign)
                {
                    columnAllSame = true;
                    for (int j = 1; j < dimensionCount; j++)
                    {
                        if (pointSigns[j, i] != firstRowSign)
                        {
                            columnAllSame = false;
                            break;
                        }
                    }
                }

                if (rowAllSame)
                {
                    Debug.Log("第" + i + "行是一样的");
                    winSign = pointSigns[i, 0];
                    sucCount++;
                    for (int j = 0; j < dimensionCount; j++)
                    {
                        PointIndex toResetIndex = new PointIndex();
                        toResetIndex.rowIndex = i;
                        toResetIndex.columnIndex = j;
                        toResetPoints.Add(toResetIndex);
                    }
                }

                if (columnAllSame)
                {
                    Debug.Log("第" + i + "列是一样的");
                    winSign = pointSigns[0, i];
                    sucCount++;
                    for (int j = 0; j < dimensionCount; j++)
                    {
                        PointIndex toResetIndex = new PointIndex();
                        toResetIndex.rowIndex = j;
                        toResetIndex.columnIndex = i;
                        if (!toResetPoints.Contains(toResetIndex))
                        {
                            toResetPoints.Add(toResetIndex);
                        }
                    }
                }
            }

            //正斜对角
            string ltSign = pointSigns[0, 0];
            bool allSame = false;
            if (ltSign != unoccupiedSign)
            {
                allSame = true;
                for (int i = 1; i < dimensionCount; i++)
                {
                    if (pointSigns[i, i] != ltSign)
                    {
                        allSame = false;
                        break;
                    }
                }
            }

            if (allSame)
            {
                Debug.Log("正对角是一样的");
                winSign = pointSigns[0, 0];
                sucCount++;
                for (int j = 0; j < dimensionCount; j++)
                {
                    PointIndex toResetIndex = new PointIndex();
                    toResetIndex.rowIndex = j;
                    toResetIndex.columnIndex = j;
                    if (!toResetPoints.Contains(toResetIndex))
                    {
                        toResetPoints.Add(toResetIndex);
                    }
                }
            }

            //反斜对角
            allSame = false;
            string rtSign = pointSigns[0, dimensionCount - 1];
            if (rtSign != unoccupiedSign)
            {
                allSame = true;
                for (int i = 1; i < dimensionCount; i++)
                {
                    if (pointSigns[i, dimensionCount - 1 - i] != rtSign)
                    {
                        allSame = false;
                        break;
                    }
                }
            }

            if (allSame)
            {
                Debug.Log("反对角是一样的");
                winSign = pointSigns[0, dimensionCount - 1];
                sucCount++;
                for (int j = 0; j < dimensionCount; j++)
                {
                    PointIndex toResetIndex = new PointIndex();
                    toResetIndex.rowIndex = j;
                    toResetIndex.columnIndex = dimensionCount - 1 - j;
                    if (!toResetPoints.Contains(toResetIndex))
                    {
                        toResetPoints.Add(toResetIndex);
                    }
                }
            }

            Debug.Log("一共有" + sucCount + "排一样的");
            if (sucCount != 0)
            {
                Debug.Log("获胜的是 " + winSign);

                MapInit.Instance.ResetPoints(toResetPoints);
                FlagData.Instance.photonView.RPC("GotScore",
                    RpcTarget.All, new object[] {winSign});
            }
        }

        public class PointIndex
        {
            public int rowIndex;
            public int columnIndex;

            public override bool Equals(object obj)
            {
                if (!(obj is PointIndex))
                {
                    return false;
                }

                PointIndex other = (PointIndex) obj;
                return rowIndex == other.rowIndex && columnIndex == other.columnIndex;
            }

            public override int GetHashCode()
            {
                return rowIndex * 137 + columnIndex * 13;
            }
        }
    }
}