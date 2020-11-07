using UnityEngine;
public abstract class Plant : CellController, ICellBehaviour
{
    public int priority;
    private int lifeTime = 0;

    public int GetPriority()
    {
        return priority;
    }

    public abstract int CalculateNextCellType();

    public abstract void SpawnNextCell();


}