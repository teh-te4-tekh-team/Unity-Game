using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{

    public QuestObject[] quests;

    public bool[] questCompleted;

    public DialogueManager dialogueManager;

    // Use this for initialization
    void Start()
    {
        this.questCompleted = new bool[this.quests.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowQuestText(string[] questText, bool clearPrevious = true)
    {
        if (clearPrevious)
        {
            this.dialogueManager.dialogueLines.Clear();
        }

        this.dialogueManager.dialogueLines.AddRange(questText);

        this.dialogueManager.currentLine = 0;
        this.dialogueManager.ShowDialogue();
    }
}
