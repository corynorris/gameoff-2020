﻿using System.Collections;
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

    [SerializeField] Plant[] objectiveResourcePrefabs;
    [SerializeField] int[] resourceObjectiveTargets;
    [SerializeField] Color[] objectiveResourceColors;

    [SerializeField] ProgressPannel losePanel;
    [SerializeField] ProgressPannel wonPanel;
    [SerializeField] ProgressPannel objectivePanel;
    [SerializeField] GameObject helpScreenPanel;

    [SerializeField] bool openHelpOnLoad;


    private TurnManager turnManager;
    private ResourceController resourceController;
    private LevelManager levelManager;
    private GridController gridController;

    private bool hasWon;
    private bool hasLost;
    private bool finishWin;
    private float winDelayTimer;
    private float winTimer;
    // Start is called before the first frame update
    void Start()
    {

        turnManager = FindObjectOfType<TurnManager>();
        resourceController = FindObjectOfType<ResourceController>();
        gridController = FindObjectOfType<GridController>();      
        
        wonPanel.Deactivate();
        losePanel.Deactivate();
        levelManager = FindObjectOfType<LevelManager>();
        turnManager.SetSpeed(1);
        levelText.text = levelManager.GetSceneName();

        hasWon = false;
        hasLost = false;
        finishWin = false;
        winDelayTimer = 0.6f;
        winTimer = 0;

        if (openHelpOnLoad)
            OpenHelpPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWon) {
            if(!hasLost)
                turnText.text = (turnLimit - turnManager.getTurnNumber()).ToString();
            if (turnManager.getTurnNumber() >= turnLimit && !hasWon)
            {
                if (!hasLost)
                {
                    EndGame();
                    hasLost = true;
                }
                

            }
           

            if (CheckPlantWinCondition() && !hasLost)
            {
                GameWon();
            }

            objectivePanel.Refresh();
        }
        else if(Time.time > winTimer+winDelayTimer && !hasLost)
        {
            wonPanel.Activate();
            finishWin = true;            
        }
        


    }

    void EndGame()
    {        


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
        //turnManager.SetSpeed(0);
        turnManager.Pause();
        losePanel.Activate();
        SoundManager.PlaySound(SoundManager.Sound.Lose, turnManager.GetSpeed());
    }

    void GameWon()
    {
        Debug.Log("You Win");
        //turnManager.SetSpeed(0);
        turnManager.Pause();
        hasWon = true;
        winTimer = Time.time;
        SoundManager.PlaySound(SoundManager.Sound.Win, turnManager.GetSpeed());
        //turnManager.SetSpeed(0);
        //wonPannel.Activate();
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
        

    public Color[] GetObjectiveResourceColors()
    {
        return objectiveResourceColors;
    }

    public void OpenHelpPanel()
    {
        helpScreenPanel.SetActive(true);
        turnManager.Pause();
    }

    public void CloseHelpPanel()
    {
        helpScreenPanel.SetActive(false);
        turnManager.Resume();
    }

}
