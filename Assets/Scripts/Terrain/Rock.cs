

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : CellController
{
    // Start is called before the first frame update
    void Start()
    {
        this.clickable = false;
    }

    public override bool IsEmpty()
    {
        return false;
    }

}