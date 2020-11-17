
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour

{    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void LoadLevel(string name)
    {
        Debug.Log("Load level requested for: " + name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("name");
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);
    }

    public void UnloadOptionsMenu()
    {
        SceneManager.UnloadSceneAsync("Options");
    }

    public void LoadLevelSelectMenu()
    {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Additive);
    }

    public void UnloadLevelSelectMenu()
    {
        SceneManager.UnloadSceneAsync("LevelSelect");
    }
}
