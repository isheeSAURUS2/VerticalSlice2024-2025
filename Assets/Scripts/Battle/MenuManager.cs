using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject battleMenuStartButton, fightMenuStartButton;
    [SerializeField] GameObject battleMenu, fightMenu;
    [SerializeField] GameObject enemyHealthCard, playerHealthCard;
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
        if (Input.GetMouseButtonDown(1)|| Input.GetMouseButtonDown(0))
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
    public void SwitchToBattleMenu() // Turn off the fight menu and turn on the rest of UI
    {
        enemyHealthCard.SetActive(true);
        playerHealthCard.SetActive(true);
        isInBattleMenu = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(battleMenuStartButton);
        fightMenu.SetActive(false);
        battleMenu.SetActive(true);
    }
    public void SwitchToFightMenu() // Turn off the battle menu and turn on the rest of UI
    {
        isInBattleMenu = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(fightMenuStartButton);
        fightMenu.SetActive(true);
        battleMenu.SetActive(false);
    }
    public void TurnOffFightMenu() // Turn off both menus leave health cards 
    {
        fightMenu.SetActive(false);
        battleMenu.SetActive(false);
    }
    public void TurnOffUI() // Turn off all UI menus
    {
        GameObject[] Menu = GameObject.FindGameObjectsWithTag("UIToDeactivate");
        for (int i = 0; i < Menu.Length; i++)
        {
            Menu[i].SetActive(false);
        }
    }
    public void ShowOnlyHealthCards() // Show only the health cards
    {
        enemyHealthCard.SetActive(true);
        playerHealthCard.SetActive(true);
        fightMenu.SetActive(false);
        battleMenu.SetActive(false);
    }
}