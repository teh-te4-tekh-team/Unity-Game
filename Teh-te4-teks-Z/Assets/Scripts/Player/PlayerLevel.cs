﻿using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {

    public GameUser gameUser;

	// Use this for initialization
	void Start () {
	    this.gameUser = new GameUser();

	    this.gameUser.Level = PlayerPrefs.GetInt("Level");
	}

    void Update()
    {
        /*if (ScoreManager.score)
        {
            
        }*/
    }

    public void LevelUp()
    {
        
    }
}
