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
    public CellController ActiveCell;
    public CellController EmptyCell;

    private CellController[,] backgroundArray;
    private CellController[,] foregroundArray;

    private static GridController _instance;
    private float offsetX;
    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        turnManager.TurnPassed += RunSimulation;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                CellController cell = hit.collider.gameObject.GetComponent<CellController>();
                SpawnCell(cell.X, cell.Y, ActiveCell);
            }
        }
    }



    void Destroy()
    {
        turnManager.TurnPassed -= RunSimulation;
    }

    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            // _instance.turnManager.TurnPassed += RunSimulation;
            ///////////////////////
            _instance.backgroundArray = new CellController[TilesWide, TilesHigh];
            _instance.foregroundArray = new CellController[TilesWide, TilesHigh];
            ///////////////////////
            List<CellController> backgroundCells = new List<CellController>(BackgroundCellObj.GetComponentsInChildren<CellController>());
            List<CellController> foregroundCells = new List<CellController>(ForegroundCellObj.GetComponentsInChildren<CellController>());




            backgroundCells.Sort((p1, p2) =>
            {
                Vector3Int p1Pos = Vector3Int.RoundToInt(p1.transform.position);
                Vector3Int p2Pos = Vector3Int.RoundToInt(p2.transform.position);
                if (p1Pos.y > p2Pos.y)
                {
                    return 1;
                }
                else if (p1Pos.y < p2Pos.y)
                {
                    return -1;
                }
                else
                {
                    if (p1Pos.x > p2Pos.x)
                    {
                        return 1;
                    }
                    else if (p1Pos.x < p2Pos.x)
                    {
                        return -1;
                    }
                }

                return 0;


            });



            int x = 0;
            int y = 0;
            float oldYPos = backgroundCells[0].transform.position.y;
            _instance.offsetX = -backgroundCells[0].transform.position.x;
            _instance.offsetY = -backgroundCells[0].transform.position.y;

            foreach (CellController backgroundCell in backgroundCells)
            {
                if (backgroundCell.transform.position.y > oldYPos)
                {
                    oldYPos = backgroundCell.transform.position.y;
                    y++;
                    x = 0;
                }
                backgroundCell.Y = y;
                backgroundCell.X = x;

                _instance.backgroundArray[x, y] = backgroundCell;
                x++;

            }

            foreach (CellController backgroundCell in backgroundCells)
            {
                bool hasForeground = false;
                foreach (CellController foregroundCell in foregroundCells)
                {
                    if (foregroundCell.transform.position == backgroundCell.transform.position)
                    {
                        foregroundCell.X = backgroundCell.X;
                        foregroundCell.Y = backgroundCell.Y;
                        _instance.foregroundArray[backgroundCell.X, backgroundCell.Y] = foregroundCell;
                        hasForeground = true;
                        break;
                    }
                }

                if (!hasForeground)
                {
                    SpawnCell(backgroundCell.X, backgroundCell.Y, EmptyCell);
                }
            }

            foreach (CellController foregroundCell in foregroundCells)
            {
                foreach (CellController backgroundCell in backgroundCells)
                {
                    if (foregroundCell.transform.position == backgroundCell.transform.position)
                    {
                        foregroundCell.X = backgroundCell.X;
                        foregroundCell.Y = backgroundCell.Y;
                        foregroundCell.Grid = _instance;

                        _instance.foregroundArray[backgroundCell.X, backgroundCell.Y] = foregroundCell;
                        break;
                    }
                }
            }

            // Create and spawn empty tileset

        }
        else
        {
            Destroy(this);
        }
    }



    public static GridController getInstance()
    {
        return _instance;
    }

    Vector2Int GetPointInDirection(int x, int y, GridDirection direction)
    {
        switch (direction)
        {
            case GridDirection.N:
                y++;
                break;
            case GridDirection.NE:
                y++;
                x++;
                break;
            case GridDirection.E:
                x++;
                break;
            case GridDirection.SE:
                y--;
                x++;
                break;
            case GridDirection.S:
                y--;
                break;
            case GridDirection.SW:
                y--;
                x--;
                break;
            case GridDirection.W:
                x--;
                break;
            case GridDirection.NW:
                y++;
                x--;
                break;
        }



        return new Vector2Int(x, y);

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

        for (int x = 0; x < TilesWide; x++)
        {
            for (int y = 0; y < TilesHigh; y++)
            {

                if (foregroundArray[x, y] is IGrows)
                {
                    // Have cell determine next spawn direction
                    ((IGrows)foregroundArray[x, y]).ClaimGrowth();
                }
            }

        }

        for (int x = 0; x < TilesWide; x++)
        {
            for (int y = 0; y < TilesHigh; y++)
            {
                if (foregroundArray[x, y].IsClaimed())
                {
                    SpawnCell(x, y, foregroundArray[x, y].GetClaimant());
                    foregroundArray[x, y].Reset();
                }
            }
        }

    }

    // Update is called once per frame


    public void OnDestroy()
    {

    }

    public void SpawnCell(int x, int y, CellController prefab)
    {
        if (x > TilesWide - 1 || y > TilesHigh - 1 || x < 0 || y < 0)
        {
            return;
        }


        if (_instance.foregroundArray[x, y] != null)
        {
            Debug.Log("destroying cell at: " + x + "," + y);
            // Destroy(_instance.foregroundArray[x, y]);
            _instance.foregroundArray[x, y].destroyCell();
        }


        Vector3 bottomLeft = backgroundArray[0, 0].transform.position;
        Vector3 position = new Vector3(bottomLeft.x + x, bottomLeft.y + y, 1);

        // Debug.Log("(" + x + ", " + y + ") - (" + origPosition.x + ", " + origPosition.y + ") - (" + bottomLeft.x + ", " + bottomLeft.y + ") ");
        // Debug.Log("(" + x + ", " + y + ")");



        CellController cell = Instantiate(prefab, position, new Quaternion());

        cell.X = x;
        cell.Y = y;
        cell.Grid = this;
        cell.transform.SetParent(ForegroundCellObj.transform, false);

        _instance.foregroundArray[x, y] = cell;


    }

}
