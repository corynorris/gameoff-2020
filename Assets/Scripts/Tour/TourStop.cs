using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TourStop : MonoBehaviour {

    [HideInInspector]
    public Tour Tour;
    
    public GameObject instructions;
    public bool AllowClickthrough = true;
    public bool enableEventSystem = false;
    
    private Collider2D targetCollider;

    void OnEnable()
    {
        if (this.Tour) { 
            this.Tour.ToggleEventSystem(enableEventSystem);
        }
    }

    private bool Clickthrough()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


        for (int i = 2; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider != this.targetCollider)
            {
                // Pretty hacky, if someone knows a better way to detect that something was clicked, while also blocking out other options lmk
                hits[i].collider.SendMessage("OnMouseDown");

            }
        }

        return hits.Length >= 3;
    }

    public void Start()
    {
        // Get collider
        targetCollider = GetComponent<Collider2D>();
    }
    
    void OnMouseDown()
    {
        if (AllowClickthrough)
        {
            if (Clickthrough())
            {
                Tour.NextStop();
            }
        }
        else
        {
            Tour.NextStop();
        }       

    }


}
