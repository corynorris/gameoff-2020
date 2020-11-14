using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private TurnManager turnManager;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseClock()
    {
        turnManager.SetSpeed(0);
    }

    public void SetNormalSpeed()
    {
        turnManager.SetSpeed(1);
    }

    public void SetFastSpeed()
    {
        turnManager.SetSpeed(2);
    }

    public void SetMaxSpeed()
    {
        turnManager.SetSpeed(3);
    }
}
