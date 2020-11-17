using System;
using UnityEngine;



public class GasManager : MonoBehaviour
{

    public int[] GasTotals = new int[GasHelpers.AllGasses.Length];


    public void ProduceGas(Gas gas, int amount)
    {
        GasTotals[(int)gas] = Mathf.Max(GasTotals[(int)gas] + amount, 0);
    }

    public void RemoveGas(Gas gas, int amount)
    {
        GasTotals[(int)gas] = Mathf.Max(GasTotals[(int)gas] - amount, 0);
    }

    public int GetTotalGas(Gas gas)
    {
        return GasTotals[(int)gas];
    }

    public void ConsumeGas(Gas gas, int amount)
    {
        GasTotals[(int)gas] = Mathf.Max(GasTotals[(int)gas] - amount, 0);
    }
}

