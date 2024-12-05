using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject battleMenuStartButton, fightMenuStartButton;
    [SerializeField] GameObject battleMenu, fightMenu;
    [SerializeField] GameObject EnemyHPCard, PlayerHPCard;
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
    public void SwitchToBattleMenu()
    {
        EnemyHPCard.SetActive(true);
        PlayerHPCard.SetActive(true);
        isInBattleMenu = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(battleMenuStartButton);
        fightMenu.SetActive(false);
        battleMenu.SetActive(true);
    }
    public void SwitchToFightMenu()
    {
        isInBattleMenu = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(fightMenuStartButton);
        fightMenu.SetActive(true);
        battleMenu.SetActive(false);
    }
    public void TurnOffFightMenu()
    {
        fightMenu.SetActive(false);
        battleMenu.SetActive(false);
    }
    public void TurnOffUI()
    {
        GameObject[] Menu = GameObject.FindGameObjectsWithTag("UIToDeactivate");
        for (int i = 0; i < Menu.Length; i++)
        {
            Menu[i].SetActive(false);
        }
    }
    public void ShowOnlyHPCard()
    {
        EnemyHPCard.SetActive(true);
        PlayerHPCard.SetActive(true);
        fightMenu.SetActive(false);
        battleMenu.SetActive(false);
    }
}