using UnityEngine;
using System.Collections;

public class QuestTrigger : MonoBehaviour {

    public QuestManager questManager;

    public int questNumber;

    public bool isQuestStarted;
    public bool isQuestEnded;


	// Use this for initialization
	void Start () {
        this.questManager = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            return;
        }

        if (other.transform.root.CompareTag("Player"))
        {
            if (!this.questManager.questCompleted[questNumber])
            {
                bool isQuestActive = this.questManager.quests[this.questNumber].gameObject.activeSelf;
                if (this.isQuestStarted && !isQuestActive)
                {
                    this.questManager.quests[this.questNumber].gameObject.SetActive(true);
                    this.questManager.quests[this.questNumber].StartQuest();
                }

                if (this.isQuestEnded && isQuestActive)
                {
                    this.questManager.quests[questNumber].EndQuest();
                }
            }
        }
    }
}
