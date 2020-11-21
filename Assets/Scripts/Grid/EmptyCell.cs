


public class EmptyCell : CellController
{

    public override bool CanBeClaimed()
    {
        return true;
    }

    public override bool IsAlive()
    {
        return false;
    }
}

