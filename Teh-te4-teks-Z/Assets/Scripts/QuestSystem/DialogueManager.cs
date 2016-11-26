using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogue;

    public bool isDialogueActive;

    public List<string>  dialogueLines;
    public int currentLine;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            this.currentLine++;
        }

        if (currentLine >= this.dialogueLines.Count)
        {
            this.dialogueBox.SetActive(false);
            this.isDialogueActive = false;
            this.currentLine = 0;
        }

        this.dialogue.text = this.dialogueLines[this.currentLine];
    }

    public void ShowDialogue()
    {
        this.isDialogueActive = true;
        this.dialogueBox.SetActive(true);
    }
}
