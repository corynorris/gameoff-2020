using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    public TabGroup tabGroup;

    public Image background;

    public bool startOpen;

    [SerializeField] Sprite diagramImage;
    
    [SerializeField] string tabText;

    [SerializeField] Sprite tabImageSelected;
    [SerializeField] Sprite tabImageHover;
    [SerializeField] Sprite tabImageIdle;

    [SerializeField] string titleTextString;
    [SerializeField] string descriptionTextString;

    [SerializeField] string expandConditionTextString;
    [SerializeField] string deathConditionTextString;

    [SerializeField] GameObject gif;    

    public Animator GetAnimator()
    {
        return gif.GetComponent<Animator>(); ;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return gif.GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelect(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public bool StartOpen()
    {
        return startOpen;
    }

    public Sprite GetTabImageSelected()
    {
        return tabImageSelected;
    }

    public Sprite GetTabImageHover()
    {
        return tabImageHover;
    }

    public Sprite GetTabImageIdle()
    {
        return tabImageIdle;
    }


    public Sprite GetDiagramImage()
    {
        return diagramImage;
    }

    public string GetTabText()
    {
        return tabText;
    }

    public string GetTitleTextString()
    {
        return titleTextString;
    }

    public string GetDescriptionTextString()
    {
        return descriptionTextString;
    }

    public string GetExpandConditionTextString()
    {
        return expandConditionTextString;
    }

    public string GetDeathConditionTextString()
    {
        return deathConditionTextString;
    }

}
