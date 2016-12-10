using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    public Text text;

    void Awake()
    {
        this.text = this.GetComponent<Text>();
        score = 0;
    }

    void Update()
    {
        this.text.text = "Score: " + score;
    }
}
