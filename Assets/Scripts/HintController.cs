﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
    [SerializeField] GameObject hints;
    private bool hintsShown;

    // Start is called before the first frame update
    void Start()
    {
        hintsShown = false;
        hints.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleHints()
    {
        if (!hintsShown)
        {
            hints.SetActive(true);
            hintsShown = true;
        }
        else
        {
            hints.SetActive(false);
            hintsShown = false;
        }
    }
}
