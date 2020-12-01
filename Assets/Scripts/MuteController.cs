using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteController : MonoBehaviour
{

    [SerializeField] GameObject muteButton;
    [SerializeField] GameObject unmuteButton;
    [SerializeField] AudioListener listener;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("mute", 0)==0)
        {
            unmute();
        }
        else
        {
            mute();
        }
    }

    public void mute()
    {
        PlayerPrefs.SetInt("mute", 1);
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
        listener.enabled = false;
    }

    public void unmute()
    {
        PlayerPrefs.SetInt("mute", 0);
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
        listener.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
