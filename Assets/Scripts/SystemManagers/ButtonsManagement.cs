using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonsManagement : MonoBehaviour
{
    [SerializeField] bool isMainButton;
    [SerializeField] TextMeshProUGUI thisText;
    [SerializeField] GameObject selectorArrow;
    [SerializeField] bool isInPokemonMenu;
    private void Start()
    {
        thisText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        selectorArrow = transform.GetChild(1).gameObject;
        if (isInPokemonMenu)
        {
            selectorArrow.GetComponent<Animator>().Play("idlePokemonMenu");
        }
        else
        {
            selectorArrow.GetComponent<Animator>().Play("idle");
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(isMainButton && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            thisText.color = Color.white;
        }
        else
        {
            thisText.color = Color.black;
        }
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            selectorArrow.SetActive(true);
        }
        else
        {
            selectorArrow.SetActive(false);
        }  
    }
}
