using UnityEngine;

public class UnOccupiedPoint : PointBase
{
    public override bool IsOccupied()
    {
        return false;
    }
}
