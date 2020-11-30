
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
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UnloadOptionsMenu()
    {
        SceneManager.UnloadSceneAsync("Options");
    }

    public void LoadLevelSelectMenu()
    {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
    }

    public void UnloadLevelSelectMenu()
    {
        SceneManager.UnloadSceneAsync("LevelSelect");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SECTOR 1");
    }
}
