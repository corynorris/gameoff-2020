
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance = null;      

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static LevelManager getInstance()
    {
        return _instance;
    }

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
}
