using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private StreamReader reader;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] public int textSelector;
    private int textSelector2;
    private int textIndex;
    private string[] currentLine;
    private string displayText;
    public float timer = 0;
    void Start()
    {
        dialogueText =  transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            DialogueTextFunction("werk dit?");
        }
    }
    public void DialogueTextFunction(string text)
    {
        timer += Time.deltaTime;
        for (int i = 0; i < text.Length; i++)
        {
            displayText += text.ToCharArray()[i];
            dialogueText.text = displayText;
            timer = 0;
        }
        //if (timer > 0.05f && textIndex < text.Length)
        //{
        //    displayText += text.ToCharArray()[textIndex];
        //    dialogueText.text = displayText;
        //    textIndex++;
        //    timer = 0;
        //}
        TurnOffDialogBox();
    }
    private void DialogueReset()
    {
        displayText = string.Empty;
        timer = 0;
        textIndex = 0;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(true);
    }
    private void TurnOffDialogBox()
    {
        displayText = string.Empty;
        dialogueText.text = displayText;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
    }
}