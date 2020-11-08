using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void winLevel()
    {

    }
    private void loseLevel() {
    }
    
}
