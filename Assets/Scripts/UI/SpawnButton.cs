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

    [SerializeField] Sprite buttonDownSprite;
    [SerializeField] Sprite buttonUpSprite;

    [SerializeField] Image buttonImage;    
    [SerializeField] Image iconImage;


    ResourceController resourceController;    

    private void Start()
    {
        resourceController = ResourceController.getInstance();
        resourceText = GetComponentInChildren<TextMeshProUGUI>();
        resourceSlider = GetComponentInChildren<Slider>();
        //buttonImage = GetComponentInChildren<Image>();        
        updateDisplay();


    }

    private void updateDisplay()
    {
        resourceTextValue = resourceCount.ToString();
        //resourceSlider.value = rechargeProgress;
        resourceText.text = resourceTextValue;
        iconImage.sprite = imageSprite;

        if (buttonStatus)
        {
            iconImage.color = Color.white;
            resourceText.color = Color.white;
            buttonImage.sprite = buttonDownSprite;
        }
        else
        {
            iconImage.color = new Color32(100, 100, 100, 255);
            resourceText.color = new Color32(100, 100, 100, 255);
            buttonImage.sprite = buttonUpSprite;
        }
            
        
    }

    private void OnMouseDown()
    {
        //  FindObjectOfType<DefenderSpawner>().setDefender(defenderPrefab);               
        if (buttonStatus)
        {
            //resourceController.ClearActiveResource();
            //buttonStatus = false;        
        }

        else
        {
            
            if(resourceCount > 0)
            {
                resourceController.SetActiveResource(resourceIndex);
                buttonImage.sprite = buttonDownSprite;
            }
                
        }
            
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
