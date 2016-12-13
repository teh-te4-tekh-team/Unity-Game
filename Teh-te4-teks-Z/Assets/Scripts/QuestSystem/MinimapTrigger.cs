using UnityEngine;
using System.Collections;

public class MinimapTrigger : MonoBehaviour
{

    public QuestManager questManager;
    public GameObject minimap;

    bool alreadySet;

    // Use this for initialization
    void Start()
    {
        this.questManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            return;
        }

        if (other.transform.root.CompareTag("Player"))
        {
            const int minimapQuestNumber = 2;
            if (this.questManager.questCompleted[minimapQuestNumber] && !alreadySet)
            {
                this.minimap.SetActive(true);
                this.alreadySet = true;
            }
        }
    }
}
