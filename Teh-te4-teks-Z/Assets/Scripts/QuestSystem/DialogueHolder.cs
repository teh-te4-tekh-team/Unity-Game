using UnityEngine;
using System.Collections.Generic;

public class DialogueHolder : MonoBehaviour {

    private DialogueManager dialogueManager;

    public List<string> dialogueLines;

	// Use this for initialization
	void Start () {
        this.dialogueManager = FindObjectOfType<DialogueManager>();
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
            if (!this.dialogueManager.isDialogueActive)
            {
                this.dialogueManager.currentLine = 0;
                this.dialogueManager.dialogueLines = this.dialogueLines;
                this.dialogueManager.ShowDialogue();
            }
        }
    }
}
