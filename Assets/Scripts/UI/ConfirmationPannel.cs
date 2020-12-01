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
    LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelController = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        //messageTextField = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        //confirmButton = gameObject.GetComponents<GameObject>()[1];
        //cancelButton = gameObject.GetComponents<GameObject>()[2];
        //turnManager = FindObjectOfType<TurnManager>();       
        //if(levelController == null)
        //    levelController = FindObjectOfType<LevelController>();


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
        if (levelController == null)
            Init();
        UpdatePannel();
        gameObject.SetActive(true);
        levelController.BlockGrid();
        gameSpeed = turnManager.GetSpeed();
        SoundManager.PlaySound(SoundManager.Sound.PositiveClick, 0.08f, 0.65f);
        turnManager.SetSpeed(0);
    }

    public void Deactivate()
    {
        if (levelController == null)
            Init();
        //UpdatePannel();
        gameObject.SetActive(false);
        turnManager.SetSpeed(gameSpeed);
        levelController.UnblockGrid();
        AudioManager.getInstance().setVolume(0.6f);
        SoundManager.PlaySound(SoundManager.Sound.NegativeClick, 0.07f, 0.6f);
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
        if (level == "")
        {
            levelManager.ReloadLevel();
        }
        else
        {
            levelManager.LoadLevel(level);
        }
    }

}
