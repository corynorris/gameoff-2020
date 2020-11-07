using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    SpawnObject spawnPrefab;
    int resourceCount = 1;
    int resourceMax = 5;    

    int resourceIndex = -1;    

    Text resourceText;
    Slider slider;
    float rechargeProgress;
    Sprite imageSprite;

    string resourceTextValue;
    bool buttonStatus = false;

    ResourceController controller;
    Image buttonImage;

    private void Start()
    {
        controller = ResourceController.getInstance();
        resourceText = GetComponentInChildren<Text>();
        slider = GetComponentInChildren<Slider>();
        buttonImage = GetComponentInChildren<Image>();
        updateDisplay();


    }

    private void updateDisplay()
    {
        resourceTextValue = resourceCount.ToString() + "/" + resourceMax.ToString();
        slider.value = rechargeProgress;
        resourceText.text = resourceTextValue;
        buttonImage.sprite = imageSprite;
        if (buttonStatus)
            buttonImage.color = Color.white;
        else
            buttonImage.color = new Color32(115, 115, 115, 255);
        
    }

    private void OnMouseDown()
    {
        //  FindObjectOfType<DefenderSpawner>().setDefender(defenderPrefab);               
        if(buttonStatus)
            controller.setActiveResource(-1);
        else
            controller.setActiveResource(resourceIndex);
    }

    void Update()
    {      
        updateDisplay();
    }


    public void setResourceCount(int count)
    {
        resourceCount = count;
    }    

    public void setResourceMax(int max)
    {
        resourceMax = max;
    }

    public void setIndex(int index)
    {        
        resourceIndex = index;
    }

    public void setRechargeProgress(float progress)
    {
        rechargeProgress = progress;
    }

    public void setActive(bool status)
    {
        buttonStatus = status;
    }

    public void setImage(Sprite sprite)
    {
        imageSprite = sprite;
        
    }
}
