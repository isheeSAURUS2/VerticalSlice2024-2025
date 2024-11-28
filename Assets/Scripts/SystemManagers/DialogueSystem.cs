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
        string filePath = Application.streamingAssetsPath + "/DialogueText.txt";

        if (File.Exists(filePath))
        {
            using (reader = new StreamReader(filePath))
            {
                currentLine = reader.ReadToEnd().Split("\n");
            }
        }
        else Debug.LogError("Text file not found!");
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 2) TurnOffDialogBox();
        if (textSelector != textSelector2)
        {
            //DialogueReset();
            if (timer > 0.05f && textIndex < currentLine[textSelector].ToCharArray().Length)
            {
                displayText += currentLine[textSelector].ToCharArray()[textIndex];
                dialogueText.text = displayText;
                textIndex++;
                timer = 0;
            }
            textSelector2 = textSelector;
        }
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
        textSelector = -1;
        dialogueText.text = displayText;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
    }
}