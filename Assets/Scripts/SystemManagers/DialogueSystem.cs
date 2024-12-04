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
    private bool dialogBoxStatus = false;
    void Start()
    {
        dialogueText =  transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TurnOffDialogBox();
    }
    public void Dialog(string text) // Function 
    {
        menuManager.TurnOffUI();
        TurnOnDialogueBox();
        textDialogue = text;
        dialogBoxStatus = true;
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 0.05f && characterIndex < textDialogue.ToCharArray().Length && dialogBoxStatus == true) // Split text into characters add 1 character to the dialogtext
        {
            dialogueText.text += textDialogue.ToCharArray()[characterIndex];
            characterIndex++;
            timer = 0;
        }
        if (timer > 2) TurnOffDialogBox();
    }
    private void TurnOnDialogueBox()
    {
        dialogueText.text = string.Empty;
        timer = 0;
        characterIndex = 0;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(true);
    }
    private void TurnOffDialogBox()
    {
        if (dialogBoxStatus == true) menuManager.SwitchToBattleMenu();
        dialogBoxStatus = false;
        dialogueText.text = string.Empty;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
    }
}