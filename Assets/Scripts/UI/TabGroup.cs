using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TabGroup : MonoBehaviour
{

    private List<TabButton> tabButtons;
    public Sprite tableIdle;
    public Sprite tableHover;
    public Sprite tableActive;

    [SerializeField] TextMeshProUGUI titleTextField;
    [SerializeField] TextMeshProUGUI descriptionTextField;
    
    [SerializeField] TextMeshProUGUI expandConditionTextField;
    [SerializeField] TextMeshProUGUI deathConditionTextField;

    [SerializeField] Image image;

    private TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();            
        }

        if (selectedTab == null && button.StartOpen())
            selectedTab = button;
         
        tabButtons.Add(button);
        ResetTabs();
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || selectedTab != button)
            button.background.sprite = button.GetTabImageHover();
    }

    public void OnTabExit(TabButton button)
    {
       ResetTabs();        
    }

    public void OnTabSelect(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = button.GetTabImageSelected();

        UpdatePanel(button);
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab != null && selectedTab != button)
                button.background.sprite = button.GetTabImageIdle();
            else
                button.background.sprite = button.GetTabImageSelected();
        }
    }   

    private void UpdatePanel(TabButton button)
    {
        titleTextField.text = button.GetTitleTextString();
        descriptionTextField.text = button.GetDescriptionTextString();

        expandConditionTextField.text = button.GetExpandConditionTextString();
        deathConditionTextField.text = button.GetDeathConditionTextString();

        image.sprite = button.GetDiagramImage();

    }

}
