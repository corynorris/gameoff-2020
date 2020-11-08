using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject CellObj;
    public GameObject ForegroundCellObj;
    public GameObject cellPrefab;
    public TurnManager turnManager;

    private CellController[,] backgroundArray;
    private CellController[,] foregroundArray;
    private List<CellController> backgroundCells;
    private List<CellController> foregroundCells;
    private static BackgroundController _instance;
    private float offsetX;
    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        turnManager.TurnPassed += RunSimulation;

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
            // _instance.turnManager.TurnPassed += RunSimulation;
            ///////////////////////
            _instance.backgroundArray = new CellController[200, 200];
            _instance.foregroundArray = new CellController[200, 200];
            ///////////////////////
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
                _instance.backgroundArray[x, y] = cell;
                x++;

            }

            foreach (CellController cell in _instance.foregroundCells)
            {
                foreach (CellController backcell in _instance.backgroundCells)
                {
                    if (cell.transform.position == backcell.transform.position)
                    {
                        cell.setPosX(backcell.getPosX());
                        cell.setPosY(backcell.getPosY());
                        _instance.foregroundArray[backcell.getPosX(), backcell.getPosY()] = cell;
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



    void RunSimulation(int turnsElapsed)
    {
        Debug.Log("Running Simulation, Turn: " + turnsElapsed);
    }

    // Update is called once per frame


    public void OnDestroy()
    {

    }

    public void spawnCell(int x, int y, Vector3 position, CellController prefab)
    {
        if (foregroundArray[x, y] != null)
        {
            return;
        }
        CellController cell = Instantiate(prefab, position, new Quaternion());
        cell.setPosX(x);
        cell.setPosY(y);
        _instance.foregroundCells.Add(cell);
        _instance.foregroundArray[x, y] = cell;
    }

    void OnMouseDown()
    {

    }
}
