using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject battleMenuStartButton, fightMenuStartButton;
    [SerializeField] GameObject battleMenu, fightMenu;
    bool isInBattleMenu = true;
    // Start is called before the first frame update

    // Update is called once per frame
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
}
