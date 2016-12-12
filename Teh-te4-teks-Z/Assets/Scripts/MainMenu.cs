using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Assets.Scripts;
using UnityEditor;
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

    public GameUser currentUser;
    public GameObject player;

    public void Login()
    {
        ApplicationUser user = new ApplicationUser();

        using (SHA512 shaM = new SHA512Managed())
        {
            byte[] hash = shaM.ComputeHash(Encoding.UTF8.GetBytes(this.Password.text));
            user.Password = Convert.ToBase64String(hash);
        }

        user.Email = this.Email.text;

        string url = "http://localhost:4861/api/User";
        string data = JsonUtility.ToJson(user);

        this.StartCoroutine(Utils.Post(url, data, this.ApplicationUserLoginAction));
    }

    void ApplicationUserLoginAction(string requestText)
    {
        if (string.IsNullOrEmpty(requestText))
        {
            this.StatusText.text = "Invalid Credentials";
            this.StatusText.color = Color.red;
        }
        else
        {
            string url = "http://localhost:4861/api/Player";
            string data = requestText;

            this.StartCoroutine(Utils.Post(url, data, this.GameUserLoginAction));
        }
    }

    void GameUserLoginAction(string requestText)
    {
        GameUser user = JsonUtility.FromJson<GameUser>(requestText);
        this.currentUser = user;

        this.Animator.SetTrigger("Logged");

        this.StatusText.text = "Username: " + user.Username;
        this.StatusText.color = Color.green;
    }

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
        string data = JsonUtility.ToJson(this.currentUser);

        string url = "http://localhost:4861/api/Player/" + this.currentUser.GameUserID;

        this.StartCoroutine(Utils.Update(url, data, this.ChangeUsernameAction));
    }

    void ChangeUsernameAction(string requestText)
    {
        this.StatusText.text = "Username: " + this.currentUser.Username;
    }
    
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void ExitGame()
    {
        Debug.Log("exit");
        //Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
