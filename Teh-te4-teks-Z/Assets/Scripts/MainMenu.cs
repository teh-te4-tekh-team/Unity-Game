using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField Email;
    public InputField Password;
    public Text Username;

    public void Login()
    {
        ApplicationUser user = new ApplicationUser();

        using (SHA512 shaM = new SHA512Managed())
        {
            byte[] hash = shaM.ComputeHash(Encoding.UTF8.GetBytes(this.Password.text));
            user.Password = Convert.ToBase64String(hash);
        }

        user.Email = this.Email.text;

        string json = JsonUtility.ToJson(user);

        this.StartCoroutine(this.GetUser(json));
    }

    IEnumerator GetUser(string json)
    {

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        byte[] pData = Encoding.ASCII.GetBytes(json.ToCharArray());

        WWW request = new WWW("http://localhost:4861/api/ApplicationUser", pData, headers);

        yield return request;

        if (request.isDone)
        {
            Debug.Log(request.text);
            StartCoroutine(GetGameUser(request.text));
        }
        
    }

    private IEnumerator GetGameUser(string json)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        byte[] pData = Encoding.ASCII.GetBytes(json.ToCharArray());

        WWW request = new WWW("http://localhost:4861/api/GameUser", pData, headers);

        yield return request;

        if (request.isDone)
        {
            Debug.Log(request.text);
            this.Username.text = "Welcome!";
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
