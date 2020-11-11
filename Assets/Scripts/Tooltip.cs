using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    private Camera uiCamera;
    private Text tooltipText;
    private RectTransform tooltipBackground;

    public void Awake()
    {
        instance = this;
        uiCamera = FindObjectOfType<Camera>();
        tooltipBackground = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
        HideTooltip();
    }

    private void Update()
    {        
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

    }

    public void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSizeVertical = 4f;
        float textPaddingSizeHorizontal = 6f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSizeHorizontal * 2f, tooltipText.preferredHeight + textPaddingSizeVertical * 2f);
        
        tooltipBackground.sizeDelta = backgroundSize;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void showTooltipStatic(string text)
    {
        instance.ShowTooltip(text);
    }

    public static void hideTooltipStatic()
    {
        instance.HideTooltip();
    }
}
