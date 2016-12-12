using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {

    public GameUser gameUser;

	// Use this for initialization
	void Start () {
	    this.gameUser = new GameUser();

	    this.gameUser.GameUserID = PlayerPrefs.GetInt("GameUserID");
	    this.gameUser.Username = PlayerPrefs.GetString("Username");
	    this.gameUser.Level = PlayerPrefs.GetInt("Level");
	    this.gameUser.HightScore = PlayerPrefs.GetInt("HighScore");
	}

    void Update()
    {
        //if (ScoreManager.score > some value)
        //{
        //    this.LevelUp();
        //}
    }

    public void LevelUp()
    {
        
    }
}
