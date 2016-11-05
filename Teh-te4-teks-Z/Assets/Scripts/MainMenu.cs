using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField Email;
    public InputField Password;
    public InputField CurrentUsername;
    public Text StatusText;
    public Animator Animator;

    private GameUser currentUser;

    public void Settings()
    {
        this.CurrentUsername.text = this.currentUser.Username;
        this.Animator.SetBool("InSettings", true);
    }

    public void BackToMenu()
    {
        this.Animator.SetBool("InSettings", false);
    }

    public void ChangeUsername()
    {
        this.currentUser.Username = this.CurrentUsername.text;
        string json = JsonUtility.ToJson(this.currentUser);

        this.StartCoroutine(this.UsernameChangeRequest(json));
    }

    IEnumerator UsernameChangeRequest(string json)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        headers.Add("X-HTTP-Method-Override", "PUT");

        byte[] pData = Encoding.ASCII.GetBytes(json.ToCharArray());

        WWW request = new WWW("http://localhost:4861/api/GameUser/" + this.currentUser.GameUserID, pData, headers);

        yield return request;

        if (request.isDone)
        {
            this.StatusText.text = "Username: " + this.currentUser.Username;
        }
    }

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
            if (string.IsNullOrEmpty(request.text))
            {
                this.StatusText.text = "Invalid Credentials";
                this.StatusText.color = Color.red;
            }
            else
            {
                this.StartCoroutine(this.GetGameUser(request.text));
            }
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
            GameUser user = JsonUtility.FromJson<GameUser>(request.text);
            this.currentUser = user;

            this.Animator.SetTrigger("Logged");

            this.StatusText.text = "Username: " + user.Username;
            this.StatusText.color = Color.green;
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
