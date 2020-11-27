﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int TilesWide;
    public int TilesHigh;
    public GameObject BackgroundCellObj;
    public GameObject ForegroundCellObj;
    public TurnManager turnManager;
    public CellController EmptyCell;
    public ResourceController ResourceController;
    public ShipController ship;
    private CellController[,] foregroundArray;
    private static GridController _instance;
    private Vector3 bottomLeft;
    private float offsetX;
    private float offsetY;

    public Dictionary<string, int> resourceTotals = new Dictionary<string, int>();

    private bool firstClick = true;
    List<CellController> cellsToUpdate = new List<CellController>();
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.Background);
        turnManager.TurnPassed += RunSimulation;
        turnManager.Pause();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {

                CellController cell = hit.collider.gameObject.GetComponent<CellController>();
                if (cell) {                    
                
                    if (foregroundArray[cell.X, cell.Y].IsClickable())
                    {
                        CellController activeResource = ResourceController.getActiveResource();

                        if (activeResource) {
                            ship.FireGun();
                            SpawnCell(cell.X, cell.Y, activeResource);
                            // Recalculate neighbours for this cell and it's neighbours
                            if (firstClick)
                            {
                                turnManager.Resume();
                                firstClick = false;
                            }
                        }
                        else
                        {
                            
                            SoundManager.PlaySound(SoundManager.Sound.CantPlace);
                        }
                    } else
                    {
                        SoundManager.PlaySound(SoundManager.Sound.CantPlace);
                    }
                } else
                {
                    SoundManager.PlaySound(SoundManager.Sound.Click);
                }
            }
        }
    }


    void Destroy()
    {
        turnManager.TurnPassed -= RunSimulation;
    }

    void Awake()
    {

        List<CellController> backgroundCells = new List<CellController>(BackgroundCellObj.GetComponentsInChildren<CellController>());
        List<CellController> foregroundCells = new List<CellController>(ForegroundCellObj.GetComponentsInChildren<CellController>());
        foregroundArray = new CellController[TilesWide, TilesHigh];
        bottomLeft = new Vector3(TilesWide, TilesHigh, 0);


 

        foreach (CellController cell in backgroundCells)
        {

            int x = Mathf.FloorToInt(cell.transform.localPosition.x);
            int y = Mathf.FloorToInt(cell.transform.localPosition.y);
            cell.X = x + (TilesWide / 2);
            cell.Y = y + (TilesHigh / 2) - 1;


            //Debug.Log("Background: " + cell.ToString());

            if (x < bottomLeft.x)
            {
                bottomLeft.x = x;
            }

            if (y < bottomLeft.y)
            {
                bottomLeft.y = y;
            }
        }

        foreach (CellController cell in foregroundCells)
        {
            int x = (int)cell.transform.localPosition.x;
            int y = (int)cell.transform.localPosition.y;
            cell.X = x + (TilesWide / 2);
            cell.Y = y + (TilesHigh / 2) - 1;

        
            AddCellToGrid(cell);
        }

        foreach (CellController cell in backgroundCells)
        {
            if (foregroundArray[cell.X, cell.Y] == null)
            {
                SpawnCell(cell.X, cell.Y, EmptyCell);
            }
        }

  

        //foreach (CellController foregroundCell in foregroundCells)
        //{

        //}

        //if (_instance == null)
        //{

        //    _instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //    // _instance.turnManager.TurnPassed += RunSimulation;
        //    ///////////////////////
        //    _instance.backgroundArray = new CellController[TilesWide, TilesHigh];
        //    _instance.foregroundArray = new CellController[TilesWide, TilesHigh];
        //    ///////////////////////
        //    List<CellController> backgroundCells = new List<CellController>(BackgroundCellObj.GetComponentsInChildren<CellController>());
        //    List<CellController> foregroundCells = new List<CellController>(ForegroundCellObj.GetComponentsInChildren<CellController>());


        //    //Debug.Log("Iterating over background cells");
        //    //foreach (CellController backgroundCell in backgroundCells)
        //    //{
        //    //    Vector3Int roundedPosition = Vector3Int.RoundToInt(backgroundCell.transform.position);
        //    //    Debug.Log(roundedPosition);
        //    //    backgroundCell.transform.position = roundedPosition;

        //    //}


        //backgroundCells.Sort((p1, p2) =>
        //{
        //    Vector3 p1Pos = p1.transform.position;
        //    Vector3 p2Pos = p2.transform.position;

        //    if (p1Pos.y > p2Pos.y)
        //    {
        //        return 1;
        //    }
        //    else if (p1Pos.y < p2Pos.y)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        if (p1Pos.x > p2Pos.x)
        //        {
        //            return 1;
        //        }
        //        else if (p1Pos.x < p2Pos.x)
        //        {
        //            return -1;
        //        }
        //    }

        //    return 0;


        //});



        //    int x = 0;
        //    int y = 0;
        //    float oldYPos = backgroundCells[0].transform.position.y;
        //    _instance.offsetX = -backgroundCells[0].transform.position.x;
        //    _instance.offsetY = -backgroundCells[0].transform.position.y;

        //    foreach (CellController backgroundCell in backgroundCells)
        //    {
        //        //Debug.Log(backgroundCell.gameObject.transform.position);
        //        if (backgroundCell.transform.position.y > oldYPos)
        //        {
        //            oldYPos = backgroundCell.transform.position.y;
        //            y++;
        //            x = 0;
        //        }
        //        backgroundCell.Y = y;
        //        backgroundCell.X = x;

        //        _instance.backgroundArray[x, y] = backgroundCell;
        //        x++;

        //    }

        //    foreach (CellController backgroundCell in backgroundCells)
        //    {
        //        bool hasForeground = false;
        //        foreach (CellController foregroundCell in foregroundCells)
        //        {
        //            if (foregroundCell.transform.position == backgroundCell.transform.position)
        //            {
        //                foregroundCell.X = backgroundCell.X;
        //                foregroundCell.Y = backgroundCell.Y;
        //                _instance.foregroundArray[backgroundCell.X, backgroundCell.Y] = foregroundCell;
        //                hasForeground = true;
        //                break;
        //            }
        //        }

        //        if (!hasForeground)
        //        {
        //            SpawnCell(backgroundCell.X, backgroundCell.Y, EmptyCell);
        //        }
        //    }

        //    foreach (CellController foregroundCell in foregroundCells)
        //    {
        //        foreach (CellController backgroundCell in backgroundCells)
        //        {
        //            if (foregroundCell.transform.position == backgroundCell.transform.position)
        //            {
        //                foregroundCell.X = backgroundCell.X;
        //                foregroundCell.Y = backgroundCell.Y;
        //                foregroundCell.Grid = _instance;

        //                _instance.foregroundArray[backgroundCell.X, backgroundCell.Y] = foregroundCell;
        //                break;
        //            }
        //        }
        //    }

        //    // Create and spawn empty tileset

        //}
        //else
        //{
        //    Destroy(this);
        //}
    }



    public static GridController getInstance()
    {
        return _instance;
    }

    public void Clear()
    {

        foreach (CellController child in this.BackgroundCellObj.GetComponentsInChildren<CellController>())      
        {
            DestroyImmediate(child.gameObject);
        }


    }


    public CellController BackgroundCell;
    [ExecuteInEditMode]
    public void GenerateGrid()
    {
        Clear();

      
        for (int y = 0; y <  TilesHigh; y++)
        {
            for (int x = 0; x < TilesWide; x++)
            {
                Vector3 offset = new Vector3(x -(TilesWide/2), y-(TilesHigh/2)+1, 0);
                Vector3 pos = BackgroundCellObj.transform.position + offset;

                CellController cell = Instantiate(BackgroundCell, pos, new Quaternion());
                cell.X = x;
                cell.Y = y;
                cell.Grid = this;
                cell.transform.SetParent(BackgroundCellObj.transform, false);
                
            }
            
        }
    }


    Vector2Int GetPointInDirection(int x, int y, GridDirection direction)
    {
        return new Vector2Int(x, y).MoveInGridDirection(direction);
    }

    public CellController GetCellInDirection(int x, int y, GridDirection direction)
    {
        Vector2Int point = GetPointInDirection(x, y, direction);

        if (point.x != Mathf.Clamp(point.x, 0, TilesWide - 1) || point.y != Mathf.Clamp(point.y, 0, TilesHigh - 1))
        {
            return null;
        }

        return foregroundArray[point.x, point.y];

    }

    // Can't write to cell directly because overwriting a cell might change the result of another cells calculation
    // Can't use a simulated flag because a cell might be overwritten

    void RunSimulation(int turnsElapsed)
    {
        cellsToUpdate.Sort();

        // Technically only need to recalculate for cells that changed and their neighbours
        for (int x = 0; x < TilesWide; x++)
        {
            for (int y = 0; y < TilesHigh; y++)
            {
                foregroundArray[x, y].RecalculateNeighbours();
            }
        }

        foreach (CellController cellController in cellsToUpdate)
        {
           cellController.ProduceEffects();
           cellController.MakeClaims();
        }

        resourceTotals.Clear();
        for (int x = 0; x < TilesWide; x++)
        {
            for (int y = 0; y < TilesHigh; y++)
            {
                if (foregroundArray[x, y].HasClaimant())
                {
                    CellController parent = foregroundArray[x, y].GetClaimant();
                    CellController child =  SpawnCell(x, y, parent);
                    foregroundArray[x, y].Initialize(parent);
                }

                if (foregroundArray[x, y].IsDead())
                {
                    SpawnCell(x, y, EmptyCell);
                }


                // Total of each plant type
                String name = foregroundArray[x, y].cellName;

                if (name.Length > 0)
                {                   
                    if (resourceTotals.ContainsKey(name))
                    {
                        resourceTotals[name]++;
                    }
                    else
                    {
                        resourceTotals[name] = 1;
                    }
                }
            }
        }


    }

    // Update is called once per frame

    private void AddCellToGrid(CellController cell) {
        cell.transform.SetParent(ForegroundCellObj.transform, false);

        cell.Grid = this;

        foregroundArray[cell.X, cell.Y] = cell;
        
        if (cell.GetPriority() > 0)
        {
            cellsToUpdate.Add(cell);
        }
    }


    public CellController SpawnCell(int x, int y, CellController prefab)
    {
        if (x > TilesWide - 1 || y > TilesHigh - 1 || x < 0 || y < 0)
        {
            return prefab;
        }


        if (foregroundArray[x, y] != null)
        {
            int indexToRemove = cellsToUpdate.IndexOf(foregroundArray[x, y]);

            if (indexToRemove > 0)
            {
                cellsToUpdate.RemoveAt(indexToRemove);
            }

            Destroy(foregroundArray[x, y].gameObject);
        }


        Vector3 scale = this.transform.localScale;
        Vector3 position = new Vector3((bottomLeft.x / scale.x) + x, (bottomLeft.y / scale.x) + y, 0.1f);

        CellController cell = Instantiate(prefab, position, new Quaternion());

        cell.X = x;
        cell.Y = y;
    
             
        AddCellToGrid(cell);

        return cell;
    }

    public Dictionary<string, int> GetResourceTotals()
    {
        return resourceTotals;
    }

}
