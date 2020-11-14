using System;
using UnityEngine;
public abstract class Plant : CellController
{
    protected bool CanClaim(CellController cellController)
    {
       return cellController && cellController.IsFree();
    }

}