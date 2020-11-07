using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandPlant : Plant
{
    public override int CalculateNextCellType()
    {

        return GetPriority();
    }

    public override void SpawnNextCell()
    {

    }


}
