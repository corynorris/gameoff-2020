using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] int turnLimit = 100;

    [SerializeField] int argonTarget = 100;
    [SerializeField] int heliumTarget = 100;
    [SerializeField] int neonTarget = 100;
    [SerializeField] int oxygenTarget = 100;

    [SerializeField] Plant[] objectiveResourcePrefabs;
    [SerializeField] int[] resourceObjectiveTargets;
    [SerializeField] int[] resourceObjectivesProgressTestResults;

    [SerializeField] ProgressPannel losePannel;
    [SerializeField] ProgressPannel wonPannel;
    [SerializeField] ProgressPannel objectivePannel;


    private TurnManager turnManager;
    private ResourceController resourceController;
    private LevelManager levelManager;
    private GridController gridController;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        resourceController = FindObjectOfType<ResourceController>();
        gridController = FindObjectOfType<GridController>();
        losePannel.Deactivate();
        wonPannel.Deactivate();
        levelManager = FindObjectOfType<LevelManager>();
        turnManager.SetSpeed(1);
        levelText.text = "Level: " + levelManager.GetSceneName();

    }

    // Update is called once per frame
    void Update()
    {
        turnText.text = "Turn " + turnManager.getTurnNumber().ToString();

        if (turnManager.getTurnNumber() >= turnLimit)
        {
            EndGame();
        }

        if (CheckPlantWinCondition())
        {
            GameWon();
        }

        objectivePannel.Refresh();
    }

    void EndGame()
    {
        Debug.Log("Game Over");
        if (argonTarget < 0 || resourceController.GetTotalGas(Gas.Argon) >= argonTarget)
        {
            if (heliumTarget < 0 || resourceController.GetTotalGas(Gas.Helium) >= heliumTarget)
            {
                if (neonTarget < 0 || resourceController.GetTotalGas(Gas.Neon) >= neonTarget)
                {
                    if (oxygenTarget < 0 || resourceController.GetTotalGas(Gas.Oxygen) >= oxygenTarget)
                    {
                        GameWon();
                        return;
                    }
                }
            }
        }

        if (CheckPlantWinCondition())
        {
            GameWon();
            return;
        }


        GameLost();
    }

    void GameLost()
    {
        Debug.Log("You Lost");
        turnManager.SetSpeed(0);
        losePannel.Activate();        
    }

    void GameWon()
    {
        Debug.Log("You Win");
        turnManager.SetSpeed(0);
        wonPannel.Activate();
    }

    private bool CheckPlantWinCondition()
    {
        if(gridController == null)
        {
            return false;
        }
        if(gridController.GetResourceTotals() == null)
        {
            return false;
        }
        for (int i = 0; i < resourceObjectiveTargets.Length; i++)
        {
            if(gridController.GetResourceTotals().ContainsKey(objectiveResourcePrefabs[i].GetType()))
            {
                if (resourceObjectiveTargets[i] > gridController.GetResourceTotals()[objectiveResourcePrefabs[i].GetType()])
                {
                    return false;
                }
            }
            else
            {
                return false;
            }          
            
        }
        return true;
    }

    public int GetObjectiveLength()
    {
        return resourceObjectiveTargets.Length;
    }

    public Plant[] GetObjectiveResourcePrefabs()
    {
        return objectiveResourcePrefabs;
    }


    public int[] GetResourceObjectiveTargets()
    {
        return resourceObjectiveTargets;
    }

    public int[] GetResourceObjectiveProgress()
    {
        return resourceObjectivesProgressTestResults;
    }

}
