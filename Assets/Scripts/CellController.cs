using System;
using UnityEngine;

public class CellController : MonoBehaviour, IComparable
{

    private int x;
    private int y;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("spawn");
    }

    [SerializeField]
    private int priority = 0;
    private CellController claimant = null;

    public void Claim(CellController claimant)
    {

        this.claimant = claimant;
        // Debug.Log("Claimed: " + this.IsClaimed());
    }

    public bool IsClaimed()
    {
        return claimant != null;
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

    public virtual bool IsEmpty()
    {
        return false;
    }

    public int GetPriority()
    {
        return priority;
    }



    public int CompareTo(object obj)
    {
        CellController other = (CellController)obj;
        return other.GetPriority().CompareTo(this.GetPriority());
    }

    public GridController Grid { set; get; }


}