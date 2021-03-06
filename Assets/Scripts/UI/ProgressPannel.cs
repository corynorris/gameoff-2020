﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressPannel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageTextField;
    [SerializeField] string messageText;

    [Header("Frame for placing goal progress")]
    [SerializeField] GameObject progressDisplayFrame;

    [SerializeField] GameObject progressDisplayPrefab;       

    private GridController gridController;
    private LevelController levelController;


    private Transform[] progressDisplayPositions;
    GameObject[] progressDisplays;
    Color[] progressColors;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        messageTextField.text = messageText;
        levelController = FindObjectOfType<LevelController>();
        progressDisplayPositions = progressDisplayFrame.GetComponentsInChildren<Transform>();        
        progressColors = levelController.GetObjectiveResourceColors();
        gridController = FindObjectOfType<GridController>();
        InitializeObjectiveBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        // WHY??
        //Debug.Log(gameObject.GetInstanceID());
        //Debug.Log(levelController);
        //Debug.Log(gridController);
    }

    private void InitializeObjectiveBlocks()
    {
        if(progressDisplays == null || progressDisplays.Length == 0)
        {
            progressDisplays = new GameObject[levelController.GetObjectiveResourcePrefabs().Length];
            for (int displayIndex = 0; displayIndex < levelController.GetObjectiveResourcePrefabs().Length; displayIndex++)
            {
                progressDisplays[displayIndex] = Instantiate(progressDisplayPrefab, progressDisplayPositions[displayIndex + 1].position, progressDisplayPositions[displayIndex + 1].rotation) as GameObject;
                progressDisplays[displayIndex].transform.SetParent(progressDisplayFrame.transform);
                progressDisplays[displayIndex].transform.localScale = progressDisplayPrefab.transform.localScale;
                progressDisplays[displayIndex].transform.localPosition = progressDisplayPositions[displayIndex + 1].localPosition;
                progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().SetObjectiveString(levelController.GetObjectiveResourcePrefabs()[displayIndex].cellName);
                progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().SetObjectiveTarget(levelController.GetResourceObjectiveTargets()[displayIndex]);
                progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().SetObjectiveProgress(0);
                progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().SetResourceImage(levelController.GetObjectiveResourcePrefabs()[displayIndex].GetComponent<SpriteRenderer>().sprite);
                progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().UpdateDisplay();
                progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().SetColor(progressColors[displayIndex]);


            }
        }

        
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Refresh();

    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

    }

    public void Refresh()
    {



        if (levelController == null || gridController == null)
        {
            Init();
        }
        
        if (levelController != null && gridController != null && gridController.GetResourceTotals() != null)
        {
            for (int displayIndex = 0; displayIndex < levelController.GetObjectiveResourcePrefabs().Length; displayIndex++)
            {
                if (gridController.GetResourceTotals().ContainsKey(levelController.GetObjectiveResourcePrefabs()[displayIndex].cellName))
                {
                  //  if(progressDisplays != null)
                        progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().SetObjectiveProgress(gridController.GetResourceTotals()[levelController.GetObjectiveResourcePrefabs()[displayIndex].cellName]);
                }
                else
                {
                   // if (progressDisplays != null)
                        progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().SetObjectiveProgress(0);
                }
                //if (progressDisplays != null)
                    progressDisplays[displayIndex].GetComponent<ProgressBlockHelper>().UpdateDisplay();
            }
        }

    }

}