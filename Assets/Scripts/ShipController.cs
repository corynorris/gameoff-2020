using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] SpriteRenderer slimeSprite;

    private Dictionary<string, Color> colorDict;
    // Start is called before the first frame update
    private TurnManager turnManager;

    private void Awake()
    {
        colorDict = new Dictionary<string, Color>();
        colorDict.Add("checkered", new Color(40 / 255f, 160 / 255f, 125 / 255f));
        colorDict.Add("edge", new Color(10 / 255f, 90 / 255f, 160 / 255f));
        colorDict.Add("fleeing", new Color(100 / 255f, 30 / 255f, 120 / 255f));
        colorDict.Add("scared", new Color(100 / 255f, 30 / 255f, 120 / 255f));
        colorDict.Add("fragileSpread", new Color(140 / 255f, 70 / 255f, 30 / 255f));
        colorDict.Add("horizontal", new Color(30 / 255f, 110 / 255f, 140 / 255f));
        colorDict.Add("vertical", new Color(30 / 255f, 110 / 255f, 140 / 255f));
        colorDict.Add("spread", new Color(165 / 255f, 50 / 255f, 50 / 255f));
    }

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PointGun();
    }

    private void PointGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = gun.transform.position - mousePos;
        direction = direction / direction.magnitude;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void FireGun()
    {
        SoundManager.PlaySound(SoundManager.Sound.Shoot, turnManager.GetSpeed(), 0.75f, 0.7f);
        gun.GetComponentInChildren<Animator>().SetTrigger("fire");
    }

    public void LoadGun(CellController cell)
    {
        slimeSprite.color = colorDict[cell.cellName];
        gun.GetComponentInChildren<Animator>().SetTrigger("loadSlime");
    }
}
