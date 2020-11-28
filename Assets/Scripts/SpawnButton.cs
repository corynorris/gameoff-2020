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

    TextMeshProUGUI resourceText;
    Slider resourceSlider;
    float rechargeProgress;
    Sprite imageSprite;

    string toolTipText;

    string resourceTextValue;
    bool buttonStatus = false;

    ResourceController resourceController;
    Image buttonImage;

    private void Start()
    {
        resourceController = ResourceController.getInstance();
        resourceText = GetComponentInChildren<TextMeshProUGUI>();
        resourceSlider = GetComponentInChildren<Slider>();
        //buttonImage = GetComponentInChildren<Image>();
        buttonImage = gameObject.transform.Find("Button Image").GetComponent<Image>();
        updateDisplay();


    }

    private void updateDisplay()
    {
        resourceTextValue = resourceCount.ToString();
        //resourceSlider.value = rechargeProgress;
        resourceText.text = resourceTextValue;
        buttonImage.sprite = imageSprite;

        if (buttonStatus)
        {
            buttonImage.color = Color.white;
            resourceText.color = Color.white;
        }
        else
        {
            buttonImage.color = new Color32(100, 100, 100, 255);
            resourceText.color = new Color32(100, 100, 100, 255);
        }
            
        
    }

    private void OnMouseDown()
    {
        //  FindObjectOfType<DefenderSpawner>().setDefender(defenderPrefab);               
        if (buttonStatus)
        {
            resourceController.ClearActiveResource();
            buttonStatus = false;        }
            
        else
            resourceController.SetActiveResource(resourceIndex);
    }

    void Update()
    {      
        updateDisplay();
    }

    public void setTooltipText(string text)
    {
        toolTipText = text;
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

    public void OnMouseEnter()
    {
        Tooltip.showTooltipStatic(toolTipText);
    }

    public void OnMouseExit()
    {
        Tooltip.hideTooltipStatic();
    }
}
