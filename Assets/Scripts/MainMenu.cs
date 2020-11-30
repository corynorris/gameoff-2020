using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip music;
    void Start()
    {
        AudioManager.getInstance().setVolume(0.6f);
        AudioManager.getInstance().playMenuMusic();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
