﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject CellObj;
    public GameObject ForegroundCellObj;
    public GameObject cellPrefab;
    private List<CellController> backgroundCells;
    private List<CellController> foregroundCells;
    private static BackgroundController _instance;
    private float offsetX;
    private float offsetY;

    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            _instance.backgroundCells = new List<CellController>(CellObj.GetComponentsInChildren<CellController>());
            _instance.foregroundCells = new List<CellController>(ForegroundCellObj.GetComponentsInChildren<CellController>());
            _instance.backgroundCells.Sort((p1, p2) =>
            {
                if (p1.transform.position.y > p2.transform.position.y)
                {
                    return 1;
                }
                else if (p1.transform.position.y == p2.transform.position.y)
                {
                    if (p1.transform.position.x > p2.transform.position.x)
                    {
                        return 1;
                    }
                    else if (p1.transform.position.x == p2.transform.position.x)
                    {
                        return 0;
                    }
                }
                return -1;
            });
            int x = 0;
            int y = 0;
            float oldYPos = _instance.backgroundCells[0].transform.position.y;
            _instance.offsetX = -_instance.backgroundCells[0].transform.position.x;
            _instance.offsetY = -_instance.backgroundCells[0].transform.position.y;
            foreach (CellController cell in _instance.backgroundCells)
            {
                if (cell.transform.position.y > oldYPos)
                {
                    oldYPos = cell.transform.position.y;
                    y++;
                    x = 0;
                }
                cell.setPosY(y);
                cell.setPosX(x);
                x++;
            }

            foreach (CellController cell in _instance.foregroundCells)
            {
                foreach (CellController backcell in _instance.backgroundCells)
                {
                    if(cell.transform.position == backcell.transform.position )
                    {
                        cell.setPosX(backcell.getPosX());
                        cell.setPosY(backcell.getPosY());
                        break;
                    }
                }                    
            }


        }
        else
        {
            Destroy(this);
        }
    }

    public static BackgroundController getInstance()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnCell(int x, int y, Vector3 position, CellController prefab)
    {
        foreach (CellController existing in foregroundCells)
        {
            if (existing.getPosX() == x && existing.getPosY() == y)
            {
                return;
            }
        }
        CellController cell = Instantiate(prefab, position, new Quaternion());
        cell.setPosX(x);
        cell.setPosY(y);
        _instance.foregroundCells.Add(cell);
    }

    void OnMouseDown()
    {

    }
}
