using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour : MonoBehaviour
{
    public GameObject eventSystem;
    public TourStop[] tourStops;

    private int currentStop = 0;
    public void Start()
    {
        ToggleEventSystem(false);

        foreach (TourStop stop in tourStops)
        {
            stop.Tour = this;
        }

        ActivateCurrentStop();
    }


    public void NextStop()
    {
        currentStop++;
        ActivateCurrentStop();
    }

    private void ActivateCurrentStop()
    {
        if (tourStops.Length == 0) return;

        for (int i = 0; i < tourStops.Length; i++)
        {
            tourStops[i].gameObject.SetActive(false);
            tourStops[i].instructions.SetActive(false);
        }

        if (currentStop < tourStops.Length)
        {
            tourStops[currentStop].gameObject.SetActive(true);
            tourStops[currentStop].instructions.SetActive(true);
        } else
        {
            ToggleEventSystem(true);
        }
    }

    public void ToggleEventSystem(bool state)
    {
        if (eventSystem)
        {
            eventSystem.SetActive(state);
        }
    }

    private void OnMouseDown()
    {
     
    }

}
