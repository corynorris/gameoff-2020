using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPannel : MonoBehaviour
{
    private string targetScene;
    private string message;
    private string confrimCallToAction;
    private string cancelCallToAction;

    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private TextMeshProUGUI messageTextField;
    [SerializeField] TurnManager turnManager;

    private float gameSpeed = 1;

    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {        
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //messageTextField = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        //confirmButton = gameObject.GetComponents<GameObject>()[1];
        //cancelButton = gameObject.GetComponents<GameObject>()[2];
        //turnManager = FindObjectOfType<TurnManager>();       
        
    }

    public void SetTargetScene(string scene)
    {
        targetScene = scene;
    }

    public void SetMessage(string text)
    {
        message = text;
    }

    public void SetConfirmCallToAction(string text)
    {
        confrimCallToAction = text;
    }

    public void SetCancelCallToAction(string text)
    {
        cancelCallToAction = text;
    }

    public void Activate()
    {
        UpdatePannel();
        gameObject.SetActive(true);
        gameSpeed = turnManager.GetSpeed();
        turnManager.SetSpeed(0);
    }

    public void Deactivate()
    {
        //UpdatePannel();
        gameObject.SetActive(false);
        turnManager.SetSpeed(gameSpeed);
    }

    private void UpdatePannel()
    {
        messageTextField.text = message;
        confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = confrimCallToAction;
        cancelButton.GetComponentInChildren<TextMeshProUGUI>().text = cancelCallToAction;

        confirmButton.GetComponent<Button>().onClick.AddListener(delegate { Action(targetScene); });
        cancelButton.GetComponent<Button>().onClick.AddListener(delegate { Deactivate(); });

    }
    public void Action(string level)
    {
        if(level == "")
        {
            levelManager.ReloadLevel();
        }
        else
        {
            levelManager.LoadLevel(level);
        }
    }

}
