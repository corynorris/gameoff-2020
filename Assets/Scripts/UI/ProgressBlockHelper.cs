using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressBlockHelper : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] TextMeshProUGUI objectiveProgressText;
    [SerializeField] Slider resourceSlider;
    [SerializeField] Image resourceImage;

    private string objectiveString;
    private int objectiveTarget = 0;
    private int objectiveProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateDisplay()
    {
        objectiveText.text = objectiveString;
        objectiveProgressText.text = objectiveProgress + " / " + objectiveTarget;        
        resourceSlider.value = (float) ((float) objectiveProgress / (float)objectiveTarget);
    }

    public void SetObjectiveString(string text)
    {
        objectiveString = text;        
    }

    public void SetObjectiveProgress(int value)
    {
        objectiveProgress = value;        
    }

    public void SetObjectiveTarget(int value)
    {
        objectiveTarget = value;        
    }

    public void SetResourceImage(Sprite img)
    {
        resourceImage.sprite = img;
    }
}
