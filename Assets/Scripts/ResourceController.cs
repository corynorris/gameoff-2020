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

    [Header("UI button frame for gas display")]
    [SerializeField] GameObject gasDisplayFrame;

    [Header("Gas display prefab")]
    [SerializeField] GameObject gasDisplayPrefab;

    [Header("Gass statistics")]
    [SerializeField] float Argon = 0.0f;
    [SerializeField] float Helium = 0.0f;
    [SerializeField] float Neon = 0.0f;
    [SerializeField] float Oxygen = 0.0f;

    [SerializeField] float ArgonChargeRate = 0.1f;
    [SerializeField] float HeliumChargeRate = 0.2f;
    [SerializeField] float NeonChargeRate = -0.1f;
    [SerializeField] float OxygenChargeRate = 0.0f;

    private ShipController ship;

    private float[] GasTotals = new float[GasHelpers.AllGasses.Length];

    SpawnButton[] spawnButtons;
    GameObject[] gasDisplays;
    Transform[] buttonPositions;
    Transform[] gasDisplayPositions;
    float[] rechargeProgress;

    int activeResourceIndex = -1;

    private static ResourceController _instance;

    void Start()
    {
        ProduceGas(Gas.Argon, Argon);
        ProduceGas(Gas.Helium, Helium);
        ProduceGas(Gas.Neon, Neon);
        ProduceGas(Gas.Oxygen, Oxygen);

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
            rechargeProgress = new float[GetButtonCount()];
            spawnButtons = new SpawnButton[GetButtonCount()];
            gasDisplays = new GameObject[GetGasLength()];
            buttonPositions = buttonFrame.GetComponentsInChildren<Transform>();
            gasDisplayPositions = gasDisplayFrame.GetComponentsInChildren<Transform>();
            InitializeButtons();
            InitializeGasDisplay();
        }
        else
        {
            Destroy(this);
        }
    }

    private void InitializeGasDisplay()
    {

        for (int displayIndex = 0; displayIndex < GetGasLength(); displayIndex++)
        {
            gasDisplays[displayIndex] = Instantiate(gasDisplayPrefab, gasDisplayPositions[displayIndex + 1].position, gasDisplayPositions[displayIndex + 1].rotation) as GameObject;
            gasDisplays[displayIndex].transform.parent = gasDisplayFrame.transform;
            gasDisplays[displayIndex].transform.localScale = gasDisplayPrefab.transform.localScale;
            gasDisplays[displayIndex].transform.localPosition = gasDisplayPositions[displayIndex + 1].localPosition;
        }
    }

    private void InitializeButtons()
    {

        for (int buttonIndex = 0; buttonIndex < GetButtonCount(); buttonIndex++)
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

    // Update is called once per frame ... this is for physics wat wat
    void FixedUpdate()
    {
        IncrementResourceCounts(1);

    }

    private int GetButtonCount()
    {
        return resourceCount.Length;
    }

    private int GetGasLength()
    {
        return GasHelpers.AllGasses.Length;
    }

    public static ResourceController getInstance()
    {
        return _instance;
    }


    
    public int NextAvailableResource()
    {
        int t = activeResourceIndex > 0 ? activeResourceIndex : 0;
        spawnButtons[t].setActive(false);

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
        ship.LoadGun(resourcePrefabs[activeResourceIndex]);
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
                return resourcePrefabs[activeResourceIndex];
            }
        }

        return null;
    }
    void IncrementResourceCounts(int turnsElapsed)
    {        
        if (spawnButtons.Length > 0)
        {
            for (int buttonIndex = 0; buttonIndex < GetButtonCount(); buttonIndex++)
            {
                if (resourceCount[buttonIndex] != resourceMax[buttonIndex])
                {
                    rechargeProgress[buttonIndex] = rechargeProgress[buttonIndex] + resourceChargeRate[buttonIndex] * Time.deltaTime;
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

        ConsumeGas(Gas.Argon, ArgonChargeRate * Time.deltaTime);
        ConsumeGas(Gas.Helium, HeliumChargeRate * Time.deltaTime);
        ConsumeGas(Gas.Neon, NeonChargeRate * Time.deltaTime);
        ConsumeGas(Gas.Oxygen, OxygenChargeRate * Time.deltaTime);

        for (int displayIndex = 0; displayIndex < GasHelpers.AllGasses.Length; displayIndex++)
        {
            gasDisplays[displayIndex].GetComponent<Text>().text = ((Gas)displayIndex).ToString() + " : " + Math.Round(GetTotalGas((Gas)displayIndex),1);
        }

    }

    public void ProduceGas(Gas gas, float amount)
    {
        GasTotals[(int)gas] = Mathf.Max(GasTotals[(int)gas] + amount, 0);
    }

    public void ConsumeGas(Gas gas, float amount)
    {
        GasTotals[(int)gas] = Mathf.Max(GasTotals[(int)gas] - amount, 0);
    }

    public float GetTotalGas(Gas gas)
    {
        return GasTotals[(int)gas];
    }
}
