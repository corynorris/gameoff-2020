using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private int posX;
    private int posY;
    private BackgroundController background;
    public CellController prefab;
    private ResourceController resourceController;

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


    // Start is called before the first frame update
    void Start()
    {
        resourceController = ResourceController.getInstance();
        background = BackgroundController.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        background.spawnCell(posX, posY, transform.position, resourceController.getActiveResource());
    }
}
