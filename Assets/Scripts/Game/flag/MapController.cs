using Frame.Utility;
using Game.flag;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private int rowCount = 3;
    private int columnCount = 3;
    private int flagDistance = 15;

    void Start()
    {
        PointJudge.Instance.Init("GreyPoint",rowCount);
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
                System.Object[] data = {position, i, j};
                ObjectPool.Instance.SpawnObj("GreyPoint", null, data);
            }
        }
    }
}