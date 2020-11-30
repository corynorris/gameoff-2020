using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlButton : MonoBehaviour
{
    [SerializeField] float gameSpeed = 1;
    [SerializeField] string toolTipText;

    [SerializeField] Sprite enabled;
    [SerializeField] Sprite diabled;

    public bool started = false;
    private TurnManager turnManager;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        
    }

    private void Update()
    {
        if (turnManager.getTurnNumber() > 0)
        {
            gameObject.GetComponent<Image>().sprite = enabled;
        }
    }

    /* 
     private void OnMouseDown()
     {
         turnManager.SetSpeed(gameSpeed);
         if (pauseGame)
         {
             turnManager.Pause();
         } else
         {
             turnManager.Resume();
         }
         UpdateColors();
     }*/

    public void UpdateSpeed()
    {
        turnManager.SetSpeed(gameSpeed);
        if (!started)
        {
            turnManager.Resume();
            SoundManager.PlaySound(SoundManager.Sound.PlayButton, turnManager.GetSpeed(), 0.8f, 0.2f);
            gameObject.GetComponent<Image>().sprite = enabled;
            started = true;
        }        
    }

    private void UpdateColors()
    {
        SpeedControlButton[] buttons = FindObjectsOfType<SpeedControlButton>();
        foreach (SpeedControlButton button in buttons)
        {
            if (button.gameSpeed != gameSpeed)
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
