using UnityEngine;
using System.Collections;

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

    public void ShowQuestText(string questText)
    {
    }
}
