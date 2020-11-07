
using UnityEngine;

public class CellController : MonoBehaviour
{
    private int posX;
    private int posY;

    public ICellBehaviour cellBehaviour;

    public int getPosX()
    {
        return this.posX;
    }

    public int getPosY()
    {
        return this.posY;
    }

    public void setPosX(int x)
    {
        this.posX = x;
    }

    public void setPosY(int y)
    {
        this.posY = y;
    }


}