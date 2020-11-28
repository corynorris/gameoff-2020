using System;
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
    private Vector3 bottomLeft;

    public Dictionary<string, int> resourceTotals = new Dictionary<string, int>();

    List<CellController> cellsToUpdate = new List<CellController>();
    // Start is called before the first frame update
    void Start()
    {
        Vector3 fgPos = ForegroundCellObj.transform.position;
        Vector3 bgPos = BackgroundCellObj.transform.position;
        ForegroundCellObj.transform.SetPositionAndRotation(new Vector3(fgPos.x, fgPos.y, bgPos.z - 0.1f), ForegroundCellObj.transform.rotation);
        SoundManager.PlaySound(SoundManager.Sound.Background, turnManager.GetSpeed());
        turnManager.TurnPassed += RunSimulation;
        turnManager.Pause();
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

            if (x < bottomLeft.x)
            {
                bottomLeft.x = x;
            }

            if (y < bottomLeft.y)
            {
                bottomLeft.y = y;
            }

            AddBackgroundCellToGrid(cell);
        }

        foreach (CellController cell in foregroundCells)
        {
            int x = (int)cell.transform.localPosition.x;
            int y = (int)cell.transform.localPosition.y;
            cell.X = x + (TilesWide / 2);
            cell.Y = y + (TilesHigh / 2) - 1;

        
            AddForegroundCellToGrid(cell);
        }

        foreach (CellController cell in backgroundCells)
        {
            if (foregroundArray[cell.X, cell.Y] == null)
            {
                SpawnCell(cell.X, cell.Y, EmptyCell);
            }
        }

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

    void RunSimulation(int turnsElapsed)
    {
        cellsToUpdate.Sort();

        for (int x = 0; x < TilesWide; x++)
        {
            for (int y = 0; y < TilesHigh; y++)
            {
                foregroundArray[x, y].RecalculateNeighbours();
            }
        }

        foreach (CellController cellController in cellsToUpdate)
        {
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
   

    private void AddBackgroundCellToGrid(CellController cell)
    {
        cell.transform.SetParent(BackgroundCellObj.transform, false);
        cell.Grid = this;
    }

    private void AddForegroundCellToGrid(CellController cell) {
        cell.transform.SetParent(ForegroundCellObj.transform, false);

        cell.Grid = this;

        foregroundArray[cell.X, cell.Y] = cell;
        
        if (cell.GetPriority() > 0)
        {
            cellsToUpdate.Add(cell);
        }
    }

    public void HandleCellClicked(CellController cell)
    {
        CellController activeResource = ResourceController.GetActiveResource();

        if (activeResource)
        {
            ship.FireGun();
            SpawnCell(cell.X, cell.Y, activeResource);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.CantPlace);
        }
    }

    private CellController SpawnCell(int x, int y, CellController prefab)
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
    
             
        AddForegroundCellToGrid(cell);

        return cell;
    }

    public Dictionary<string, int> GetResourceTotals()
    {
        return resourceTotals;
    }

}
