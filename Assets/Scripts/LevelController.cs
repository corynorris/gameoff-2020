using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI turnText;

    private TurnManager turnManager;
    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        turnText.text = "Turn " + turnManager.getTurnNumber().ToString();
    }
}
