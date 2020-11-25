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
    
    [SerializeField] ProgressPannel losePannel;
    [SerializeField] ProgressPannel wonPannel;
    [SerializeField] ProgressPannel objectivePannel;


    private TurnManager turnManager;
    private ResourceController resourceController;
    private LevelManager levelManager;
    private GridController gridController;

    private bool hasWon;
    private bool finishWin;
    private float winDelayTimer;
    private float winTimer;
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

        hasWon = false;
        finishWin = false;
        winDelayTimer = 1.5f;
        winTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWon) {
            turnText.text = "Turn " + turnManager.getTurnNumber().ToString();
            if (turnManager.getTurnNumber() >= turnLimit && !hasWon)
            {
                EndGame();
            }

            if (CheckPlantWinCondition())
            {
                GameWon();
            }

            objectivePannel.Refresh();
        }
        else if(Time.time > winTimer+winDelayTimer)
        {
            wonPannel.Activate();
            finishWin = true;
        }
        


    }

    void EndGame()
    {        
        /*if (argonTarget < 0 || resourceController.GetTotalGas(Gas.Argon) >= argonTarget)
        {
            if (heliumTarget < 0 || resourceController.GetTotalGas(Gas.Helium) >= heliumTarget)
            {
                if (neonTarget < 0 || resourceController.GetTotalGas(Gas.Neon) >= neonTarget)
                {
                    if (oxygenTarget < 0 || resourceController.GetTotalGas(Gas.Oxygen) >= oxygenTarget)
                    {
                        Debug.Log("Game won2");
                        GameWon();
                        return;
                    }
                }
            }
        }*/

        if (CheckPlantWinCondition())
        {
            GameWon();
            Debug.Log("Game won");
            return;
        }

        Debug.Log("Game lost");
        GameLost();
        return;
    }

    void GameLost()
    {
        Debug.Log("You Lost");
        turnManager.SetSpeed(0);
        losePannel.Activate();
        SoundManager.PlaySound(SoundManager.Sound.Lose);
    }

    void GameWon()
    {
        Debug.Log("You Win");
        //turnManager.SetSpeed(0);
        hasWon = true;
        winTimer = Time.time;
        SoundManager.PlaySound(SoundManager.Sound.Win);
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
            if(gridController.GetResourceTotals().ContainsKey(objectiveResourcePrefabs[i].cellName))
            {
                if (resourceObjectiveTargets[i] > gridController.GetResourceTotals()[objectiveResourcePrefabs[i].cellName])
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
        

}
