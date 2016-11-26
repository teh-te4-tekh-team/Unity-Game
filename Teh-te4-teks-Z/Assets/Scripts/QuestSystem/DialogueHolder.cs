using UnityEngine;
using System.Collections;

public class DialogueHolder : MonoBehaviour {

    public string dialogue;
    private DialogueManager dialogueManager;

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
            this.dialogueManager.ShowBox(this.dialogue);
        }
    }
}
