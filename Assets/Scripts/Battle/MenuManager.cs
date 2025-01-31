using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject battleMenuStartButton, fightMenuStartButton, pokemonMenuStartButton;
    [SerializeField] GameObject battleMenu, fightMenu;
    [SerializeField] GameObject enemyHPCard, playerHPCard;
    [SerializeField] GameObject pokemonUIButtons, pokemonMenuBack;
    
    //[SerializeField] GameObject pokemonUI, SnorlaxUI;
    bool isInBattleMenu = true;
    public bool inBattleSequence = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (!isInBattleMenu)
            {
                SwitchToBattleMenu();
            }
        }
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            if (!isInBattleMenu)
            {
                SwitchToFightMenu();
            }
            else
            {
                SwitchToBattleMenu();
            }
        }
    }
    public void SwitchToBattleMenu()
    {
        enemyHPCard.SetActive(true);
        playerHPCard.SetActive(true);
        isInBattleMenu = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(battleMenuStartButton);
        fightMenu.SetActive(false);
        battleMenu.SetActive(true);
        pokemonMenuBack.SetActive(false);
        pokemonUIButtons.SetActive(false);
    }
    public void SwitchToFightMenu()
    {
        isInBattleMenu = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(fightMenuStartButton);
        fightMenu.SetActive(true);
        battleMenu.SetActive(false);
        pokemonMenuBack.SetActive(false);
        pokemonUIButtons.SetActive(false);
    }
    public void TurnOffFightMenu()
    {
        fightMenu.SetActive(false);
        battleMenu.SetActive(false);
        //pokemonUI.SetActive(false);
        //SnorlaxUI.SetActive(false);
    }
    public void TurnOffUI()
    {
        GameObject[] Menu = GameObject.FindGameObjectsWithTag("UIToDeactivate");
        for (int i = 0; i < Menu.Length; i++)
        {
            Menu[i].SetActive(false);
        }
        //pokemonUI.SetActive(false);
        //SnorlaxUI.SetActive(false);
    }
    public void ShowOnlyHealthCards()
    {
        enemyHPCard.SetActive(true);
        playerHPCard.SetActive(true);
        fightMenu.SetActive(false);
        battleMenu.SetActive(false);
    }
    public void SwitchToPokemonMenu()
    {
        pokemonUIButtons.SetActive(true);
        pokemonMenuBack.SetActive(true);
        enemyHPCard.SetActive(false);
        playerHPCard.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pokemonMenuStartButton);
        fightMenu.SetActive(false);
        battleMenu.SetActive(false);
    }
}