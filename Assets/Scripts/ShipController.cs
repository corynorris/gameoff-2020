using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        
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
        SoundManager.PlaySound(SoundManager.Sound.Shoot);
        gun.GetComponentInChildren<Animator>().SetTrigger("fire");
    }
}
