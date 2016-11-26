using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public bool dialogueActive;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            this.dialogueBox.SetActive(false);
            this.dialogueActive = false;
        }
    }

    public void ShowBox(string dialogue)
    {
        this.dialogueActive = true;
        this.dialogueBox.SetActive(true);
        this.dialogueText.text = dialogue;
    }
}
