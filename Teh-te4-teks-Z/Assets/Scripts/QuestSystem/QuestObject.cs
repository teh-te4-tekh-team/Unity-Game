﻿using UnityEngine;
using System.Collections;

public class QuestObject : MonoBehaviour
{
    public int questNumber;

    public QuestManager questManager;

    public string[] startText;
    public string[] endText;

    public bool clearPreviousQuestText;

    // Use this for initialization
    void Start()
    {
        this.questManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartQuest()
    {
        this.questManager.ShowQuestText(this.startText, this.clearPreviousQuestText);
    }

    public void EndQuest()
    {
        this.questManager.ShowQuestText(this.endText);
        this.questManager.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
