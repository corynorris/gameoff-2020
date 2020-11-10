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
    [SerializeField] SpawnButton buttonPrefab;
    [SerializeField] GameObject buttonFrame;


    SpawnButton[] spawnButtons;
    Transform[] buttonPositions;
    float[] rechargeProgress;

    int activeResourceIndex = -1;

    private static ResourceController _instance;

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
        for (int i = 0; i < getButtonCount(); i++)
        {
            spawnButtons[i] = Instantiate(buttonPrefab, buttonPositions[i + 1].position, buttonPositions[i + 1].rotation) as SpawnButton;
            spawnButtons[i].transform.parent = buttonFrame.transform;
            spawnButtons[i].transform.localScale = buttonPrefab.transform.localScale;
            spawnButtons[i].transform.localPosition = buttonPositions[i + 1].localPosition;
            spawnButtons[i].setResourceCount(resourceCount[i]);
            spawnButtons[i].setResourceMax(resourceMax[i]);
            spawnButtons[i].setRechargeProgress(rechargeProgress[i]);
            spawnButtons[i].setIndex(i);
            spawnButtons[i].setImage(buttonSprites[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnButtons.Length > 0)
        {
            for (int i = 0; i < getButtonCount(); i++)
            {
                if (resourceCount[i] != resourceMax[i])
                {
                    rechargeProgress[i] = rechargeProgress[i] + resourceChargeRate[i];
                    if (rechargeProgress[i] >= 1)
                    {
                        resourceCount[i]++;
                        rechargeProgress[i] = 0;
                    }
                    spawnButtons[i].setResourceCount(resourceCount[i]);
                    spawnButtons[i].setResourceMax(resourceMax[i]);
                    spawnButtons[i].setRechargeProgress(rechargeProgress[i]);
                }
            }
        }

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
        for (int i = 0; i < getButtonCount(); i++)
        {
            if (i != activeResourceIndex)
                spawnButtons[i].setActive(false);
            if (i == activeResourceIndex)
                spawnButtons[i].setActive(true);
        }
    }

    public void clearActiveResource(int index)
    {
        activeResourceIndex = -1;
    }

    public CellController getActiveResource()
    {
        if (activeResourceIndex >= 0)
        {
            if (resourceCount[activeResourceIndex] > 0)
            {
                resourceCount[activeResourceIndex]--;
                resourceMax[activeResourceIndex]--;
                return resourcePrefabs[activeResourceIndex];
            }
        }
        return null;
    }
}
