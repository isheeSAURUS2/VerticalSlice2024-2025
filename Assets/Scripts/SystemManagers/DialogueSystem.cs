using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    private TextMeshProUGUI dialogueText;
    private int characterIndex;
    private float timer = 0;
    private string textDialogue = "";
    private bool dialogueBoxStatus = false;
    void Start()
    {
        dialogueText =  transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TurnOffDialogBox();
        menuManager.SwitchToBattleMenu();
    }
    public void Dialogue(string text) // Function for use the dialogue 
    {
        menuManager.TurnOffUI();
        TurnOnDialogueBox();
        textDialogue = text;
        dialogueBoxStatus = true;
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 0.05f && characterIndex < textDialogue.ToCharArray().Length && dialogueBoxStatus == true) // Split text into characters add 1 character to the dialogtext
        {
            dialogueText.text += textDialogue.ToCharArray()[characterIndex];
            characterIndex++;
            timer = 0;
        }
        if (timer >= 2 && dialogueBoxStatus && !menuManager.inBattleSequence) TurnOffDialogBox();
        if (timer >= 2 && menuManager.inBattleSequence && dialogueBoxStatus)
        {
            TurnOffDialogueBoxInBattle();
        };
    }
    private void TurnOnDialogueBox() // Turn on the dialogue box
    {
        menuManager.TurnOffUI();
        dialogueText.text = "";
        timer = 0;
        characterIndex = 0;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(true);
    }
    private void TurnOffDialogBox() // Turn off Dialogue box out of battle
    {
        menuManager.SwitchToBattleMenu();
        dialogueBoxStatus = false;
        dialogueText.text = string.Empty;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
    }
    private void TurnOffDialogueBoxInBattle() // Turn off Dialogue box in battle
    {
        menuManager.ShowOnlyHealthCards();
        dialogueBoxStatus = false;
        dialogueText.text = string.Empty;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
    }
}