using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{

    [SerializeField] AudioClip music;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject topText;
    [SerializeField] GameObject middleText;
    [SerializeField] GameObject bottomText;

    private float timer;
    private float volume;
    // Start is called before the first frame update
    void Start()
    {
        volume = 0.6f;
        AudioManager.getInstance().setVolume(volume);
        AudioManager.getInstance().changeMusic(music);
        timer = Time.time;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer + (music.length * Time.timeScale) - 2 < Time.time)
        {
            volume -= 0.01f;
            AudioManager.getInstance().setVolume(volume);
        }
        if (timer + (music.length* Time.timeScale) + 0.5< Time.time)
        {
            levelManager.LoadLevel("Menu");
        }

        if (timer + 5 < Time.time)
        {
            if (topText.GetComponent<Text>().color.a <= 0) { 
            StartCoroutine(FadeTextToFullAlpha(6f, topText.GetComponent<Text>()));
            }
        }
        if (timer + 9.5 < Time.time)
        {

            if (middleText.GetComponent<Text>().color.a <= 0)
            {
                StartCoroutine(FadeTextToFullAlpha(4f, middleText.GetComponent<Text>()));
            }
        }
        if (timer + 14 < Time.time)
        {

            if (bottomText.GetComponent<Text>().color.a <= 0)
            {
                StartCoroutine(FadeTextToFullAlpha(4f, bottomText.GetComponent<Text>()));
            }
        }

    }


    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
