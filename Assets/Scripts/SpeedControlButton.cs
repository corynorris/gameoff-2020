using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlButton : MonoBehaviour
{
    [SerializeField] int gameSpeed = 1;
    [SerializeField] string toolTipText;


    private TurnManager turnManager;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        
    }

    private void OnMouseDown()
    {
        turnManager.SetSpeed(gameSpeed);
        UpdateColors();
    }

    private void UpdateColors()
    {
        SpeedControlButton[] buttons = FindObjectsOfType<SpeedControlButton>();
        foreach (SpeedControlButton button in buttons)
        {
            if (button.gameSpeed == gameSpeed)
            {
                button.GetComponent<Image>().color = Color.white;

            }
            else
            {
                button.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            }
        }
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
