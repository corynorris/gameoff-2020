using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Image image;
    public float interval = 1f;
    public Color flashColor;

    private TurnManager turnManager;
    private ResourceController resourceController;


    private bool toggleFlashing = false;
    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        resourceController = FindObjectOfType<ResourceController>();
        resourceController.HopperEmpty += BeginFlash;
    }

    private void BeginFlash()
    {
        Debug.Log("Begin Flash");
        StopAllCoroutines();
        StartCoroutine("Flash");
    }

    IEnumerator Flash()
    {
        Debug.Log("Flash");
        image.color = flashColor;
        yield return new WaitForSeconds(interval);
    }

    private void OnMouseDown()
    {
        StopAllCoroutines();
    }

    void Destroy()
    {
        resourceController.HopperEmpty -= BeginFlash;
    }



}
