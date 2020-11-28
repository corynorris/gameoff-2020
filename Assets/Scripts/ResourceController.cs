using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    [Header("Spawn options")]
    [SerializeField] CellController[] resourcePrefabs;
    [SerializeField] int[] resourceMax;
    [SerializeField] Sprite[] buttonSprites;
    [SerializeField] float[] resourceChargeRate;
    [SerializeField] int[] resourceCount;
    [SerializeField] string[] tooltipText;

    [Header("Spawn button prefab")]
    [SerializeField] SpawnButton buttonPrefab;

    [Header("UI button frame for button deployment")]
    [SerializeField] GameObject buttonFrame;


    private ShipController ship;

    SpawnButton[] spawnButtons;
    Transform[] buttonPositions;

    int activeResourceIndex = -1;

    private static ResourceController _instance;

    void Start()
    {

        activeResourceIndex = NextAvailableResource();

        ship = FindObjectOfType<ShipController>();
        ship.LoadGun(resourcePrefabs[activeResourceIndex]);
    }


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
            spawnButtons = new SpawnButton[GetButtonCount()];
            buttonPositions = buttonFrame.GetComponentsInChildren<Transform>();
            InitializeButtons();
        }
        else
        {
            Destroy(this);
        }
    }


    private void InitializeButtons()
    {

        for (int buttonIndex = 0; buttonIndex < GetButtonCount(); buttonIndex++)
        {

            spawnButtons[buttonIndex] = Instantiate(buttonPrefab, buttonPositions[buttonIndex + 1].position, buttonPositions[buttonIndex + 1].rotation) as SpawnButton;
            spawnButtons[buttonIndex].transform.SetParent(buttonFrame.transform);
            spawnButtons[buttonIndex].transform.localScale = buttonPrefab.transform.localScale;
            spawnButtons[buttonIndex].transform.localPosition = buttonPositions[buttonIndex + 1].localPosition;
            spawnButtons[buttonIndex].setResourceCount(resourceCount[buttonIndex]);
            spawnButtons[buttonIndex].setResourceMax(resourceMax[buttonIndex]);
            spawnButtons[buttonIndex].setIndex(buttonIndex);
            spawnButtons[buttonIndex].setImage(buttonSprites[buttonIndex]);
            spawnButtons[buttonIndex].setTooltipText(tooltipText[buttonIndex]);

        }
    }

    private int GetButtonCount()
    {
        return resourceCount.Length;
    }


    public static ResourceController getInstance()
    {
        return _instance;
    }


    
    public int NextAvailableResource()
    {
        int t = activeResourceIndex > 0 ? activeResourceIndex : 0;
    

        for (int i = 0; i < resourceCount.Length; i++)
        {
            if (resourceCount[t] > 0)
            {
                
                spawnButtons[t].setActive(true);
                return t;
            }
            t = t > (resourceCount.Length - 2) ? 0 : t + 1;

        }

        return -1;
    }

    public void SetActiveResource(int index)
    {        
        activeResourceIndex = index;
        for (int buttonIndex = 0; buttonIndex < GetButtonCount(); buttonIndex++)
        {
            if (buttonIndex != activeResourceIndex)
                spawnButtons[buttonIndex].setActive(false);
            if (buttonIndex == activeResourceIndex)
                spawnButtons[buttonIndex].setActive(true);
        }
        if (activeResourceIndex >= 0) { 
            ship.LoadGun(resourcePrefabs[activeResourceIndex]);
        } 
    }

    public void ClearActiveResource()
    {
        activeResourceIndex = -1;
    }

    public CellController GetActiveResource()
    {

        activeResourceIndex = NextAvailableResource();

        if (activeResourceIndex >= 0)
        {
            if (resourceCount[activeResourceIndex] > 0)
            {
                resourceCount[activeResourceIndex] = resourceCount[activeResourceIndex] - 1;
                resourceMax[activeResourceIndex] = resourceMax[activeResourceIndex] - 1;
                spawnButtons[activeResourceIndex].setResourceCount(resourceCount[activeResourceIndex]);
                spawnButtons[activeResourceIndex].setResourceMax(resourceMax[activeResourceIndex]);

                int prevActiveResourceIndex = activeResourceIndex;
                SetActiveResource(NextAvailableResource());
                return resourcePrefabs[prevActiveResourceIndex];
            }
        }

        

        return null;
    }
    

}
