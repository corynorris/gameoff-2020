using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] CellController[] resourcePrefabs;
    [SerializeField] int[] resourceMax;
    [SerializeField] Sprite[] buttonSprites;
    [SerializeField] float[] resourceChargeRate;
    [SerializeField] int[] resourceCount;
    [SerializeField] string[] tooltipText;
    [SerializeField] SpawnButton buttonPrefab;
    [SerializeField] GameObject buttonFrame;

    public TurnManager turnManager;

    SpawnButton[] spawnButtons;
    Transform[] buttonPositions;
    float[] rechargeProgress;

    int activeResourceIndex = -1;

    private static ResourceController _instance;

    void Start()
    {
        turnManager.TurnPassed += IncrementResourceCounts;

    }

    void Destroy()
    {
        turnManager.TurnPassed -= IncrementResourceCounts;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            rechargeProgress = new float[getButtonCount()];
            spawnButtons = new SpawnButton[getButtonCount()];
            buttonPositions = buttonFrame.GetComponentsInChildren<Transform>();
            initializeButtons();
        }
        else
        {
            Destroy(this);
        }
    }


    private void initializeButtons()
    {

        for (int buttonIndex = 0; buttonIndex < getButtonCount(); buttonIndex++)
        {

            spawnButtons[buttonIndex] = Instantiate(buttonPrefab, buttonPositions[buttonIndex + 1].position, buttonPositions[buttonIndex + 1].rotation) as SpawnButton;
            spawnButtons[buttonIndex].transform.parent = buttonFrame.transform;
            spawnButtons[buttonIndex].transform.localScale = buttonPrefab.transform.localScale;
            spawnButtons[buttonIndex].transform.localPosition = buttonPositions[buttonIndex + 1].localPosition;
            spawnButtons[buttonIndex].setResourceCount(resourceCount[buttonIndex]);
            spawnButtons[buttonIndex].setResourceMax(resourceMax[buttonIndex]);
            spawnButtons[buttonIndex].setRechargeProgress(rechargeProgress[buttonIndex]);
            spawnButtons[buttonIndex].setIndex(buttonIndex);
            spawnButtons[buttonIndex].setImage(buttonSprites[buttonIndex]);
            spawnButtons[buttonIndex].setTooltipText(tooltipText[buttonIndex]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        // IncrementResourceCounts(1);

    }

    private int getButtonCount()
    {
        return resourceCount.Length;
    }

    public static ResourceController getInstance()
    {
        return _instance;
    }

    public void setActiveResource(int index)
    {

        activeResourceIndex = index;
        for (int buttonIndex = 0; buttonIndex < getButtonCount(); buttonIndex++)
        {
            if (buttonIndex != activeResourceIndex)
                spawnButtons[buttonIndex].setActive(false);
            if (buttonIndex == activeResourceIndex)
                spawnButtons[buttonIndex].setActive(true);
        }
    }

    public void clearActiveResource()
    {
        activeResourceIndex = -1;
    }

    public CellController getActiveResource()
    {
        if (activeResourceIndex >= 0)
        {
            if (resourceCount[activeResourceIndex] > 0)
            {
                resourceCount[activeResourceIndex] = resourceCount[activeResourceIndex] - 1;
                resourceMax[activeResourceIndex] = resourceMax[activeResourceIndex] - 1;
                spawnButtons[activeResourceIndex].setResourceCount(resourceCount[activeResourceIndex]);
                spawnButtons[activeResourceIndex].setResourceMax(resourceMax[activeResourceIndex]);
                return resourcePrefabs[activeResourceIndex];
            }
        }
        return null;
    }
    void IncrementResourceCounts(int turnsElapsed)
    {
        Debug.Log("here");
        if (spawnButtons.Length > 0)
        {
            for (int buttonIndex = 0; buttonIndex < getButtonCount(); buttonIndex++)
            {
                if (resourceCount[buttonIndex] != resourceMax[buttonIndex])
                {
                    rechargeProgress[buttonIndex] = rechargeProgress[buttonIndex] + resourceChargeRate[buttonIndex];
                    if (rechargeProgress[buttonIndex] >= 1)
                    {
                        resourceCount[buttonIndex]++;
                        rechargeProgress[buttonIndex] = 0;
                    }
                    spawnButtons[buttonIndex].setResourceCount(resourceCount[buttonIndex]);
                    spawnButtons[buttonIndex].setResourceMax(resourceMax[buttonIndex]);
                    spawnButtons[buttonIndex].setRechargeProgress(rechargeProgress[buttonIndex]);

                }
            }
        }
    }
}
