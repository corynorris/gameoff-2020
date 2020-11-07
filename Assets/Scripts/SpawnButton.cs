using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] SpawnObject spawnPrefab;
    [SerializeField] int countAvailable = 1;
    [SerializeField] int countMax = 5;
    [SerializeField] int countStart = 1;
    [SerializeField] float rechargeSpeed = 1;

    Text resourceText;
    Slider slider;

    string resourceTextValue;

    private void Start()
    {      

        resourceText = GetComponentInChildren<Text>();
        slider = GetComponentInChildren<Slider>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        resourceTextValue = countAvailable.ToString() + "/" + countMax.ToString();        
        resourceText.text = resourceTextValue;
    }

    private void OnMouseDown()
    {      
      //  FindObjectOfType<DefenderSpawner>().setDefender(defenderPrefab);
    }

    void Update()
    {
        if(slider.enabled)
            slider.value = slider.value + rechargeSpeed;

        if(slider.value >= slider.maxValue)
        {
            countAvailable++;
            if (countAvailable == countMax)
                slider.enabled = false;
            
            slider.value = 0;
            UpdateDisplay();
        }
    }
     
}
