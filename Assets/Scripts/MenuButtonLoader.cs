using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonLoader : MonoBehaviour
{
    [SerializeField] string message;
    
    [SerializeField] string confirmationText;
    [SerializeField] string cancelText;

    [SerializeField] string sceneTarget;

    [SerializeField] ConfirmationPannel pannel;
    

    // Start is called before the first frame update
    void Start()
    {
        //pannel = FindObjectOfType<ConfirmationPannel>();
        //Debug.Log(pannel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {        
        pannel.SetCancelCallToAction(cancelText);
        pannel.SetConfirmCallToAction(confirmationText);
        pannel.SetMessage(message);
        pannel.SetTargetScene(sceneTarget);
        pannel.Activate();        
    }
}
