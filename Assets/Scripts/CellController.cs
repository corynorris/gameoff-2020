using UnityEngine;

public class CellController : MonoBehaviour
{

    [SerializeField]
    private int x;
    [SerializeField]
    private int y;



    private CellController claimant = null;


    public void Claim(CellController claimant)
    {
        this.claimant = claimant;
    }

    public bool IsClaimed()
    {
        return claimant != null;
    }

    public bool IsEmpty()
    {
        return false;
    }

    public CellController GetClaimant()
    {
        return claimant;
    }

    public void Reset()
    {
        this.claimant = null;
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public GridController Grid { set; get; }


}