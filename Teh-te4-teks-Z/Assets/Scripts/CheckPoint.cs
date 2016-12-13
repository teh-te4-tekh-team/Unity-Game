using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class CheckPoint : MonoBehaviour
{
    public GameObject player;
    private GameUser gameUser;

	// Use this for initialization
	void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player");
	    this.gameUser = new GameUser();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.name != "PlayerModel") return;

        if (other.transform.root.CompareTag("Player"))
        {
            string checkPointPosition = JsonUtility.ToJson(this.transform.position);
            this.SaveCheckPoint(checkPointPosition);
        }
    }

    void SaveCheckPoint(string checkPointPosition)
    {
        this.gameUser.GameUserID = PlayerPrefs.GetInt("GameUserID");
        this.gameUser.Username = PlayerPrefs.GetString("Username");
        this.gameUser.CheckPointPosition = checkPointPosition;

        string json = JsonUtility.ToJson(this.gameUser);

        //Debug.Log(json);

        this.StartCoroutine(this.SavePosition(json));
    }

    IEnumerator SavePosition(string json)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        headers.Add("X-HTTP-Method-Override", "PUT");

        byte[] pData = Encoding.ASCII.GetBytes(json.ToCharArray());

        WWW request = new WWW("http://localhost:4861/api/GameUser/" + this.gameUser.GameUserID, pData, headers);

        yield return request;

        if (request.isDone)
        {
            Debug.Log(request.isDone);
        }
    }
}
