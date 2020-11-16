using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public delegate void TurnPassHandler(int ticksElapsed);
    public event TurnPassHandler TurnPassed; // event

    [SerializeField] int[] timingOptions = {1, 2, 3, 4};
    [SerializeField] float turnFrequency = 1f;
    public float TurnFrequency
    {
        get { return turnFrequency; }
        set
        {
            timeElapsed = 0f;
            turnFrequency = value;
        }
    }

    [SerializeField] private bool running = false;
    private int turnsElapsed = 0;
    private float timeElapsed = 0f;


    void Update()
    {
        if (!running) return;

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= TurnFrequency)
        {

            timeElapsed = timeElapsed % TurnFrequency;
            NotifyTurnPassed();
        }
    }

    void NotifyTurnPassed()
    {
        turnsElapsed += 1;
        TurnPassed?.Invoke(turnsElapsed);
    }

    public void Resume()
    {
        running = true;
    }

    public void Pause()
    {
        running = false;
    }

    public void SetSpeed(int speed)
    {
        Time.timeScale = speed;
    }

    public int getTurnNumber()
    {
        return turnsElapsed;
    }

}
