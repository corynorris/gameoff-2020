using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Image image;
    public float interval = 0.4f;
    public Color flashColor;
    private Color originalColor;
    private Color targetColor;

    private TurnManager turnManager;
    private ResourceController resourceController;


    private bool isFlashing = false;
    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        resourceController = FindObjectOfType<ResourceController>();
        resourceController.HopperEmpty += BeginFlash;
        turnManager.TurnPassed += EndFlash;
    }

    private void BeginFlash()
    {
        isFlashing = true;
        originalColor = image.color;
        StopAllCoroutines();
        StartCoroutine("Flash");
    }

    private void EndFlash(int time)
    {
        if (isFlashing) {
            isFlashing = false;
            image.color = originalColor;
            StopAllCoroutines();
        }
    }

    IEnumerator Flash()
    {
        while (isFlashing) {
            
            float t = 0;

            Color startColor = image.color;

            if (image.color == flashColor)
            {
                targetColor = originalColor;
            }
            else
            {
                targetColor = flashColor;
            }

            while (t < interval)
            {
                t += Time.deltaTime;
                image.color = Color.Lerp(startColor, targetColor, t / interval);
                yield return null;
            }

        }
    }


    void Destroy()
    {
        resourceController.HopperEmpty -= BeginFlash;
        turnManager.TurnPassed -= EndFlash;
    }



}
