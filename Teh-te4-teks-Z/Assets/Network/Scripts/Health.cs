using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    public bool DestroyOnDeath;

    public NetworkStartPosition[] NetworkStartPositions;

    void Start()
    {
        if (this.isLocalPlayer)
        {
            this.NetworkStartPositions = FindObjectsOfType<NetworkStartPosition>();
        }
    }
    public void TakeDamage(int amount)
    {
        if (!this.isServer)
        {
            return;
        }

        this.currentHealth -= amount;

        if (this.currentHealth <= 0)
        {
            if (this.DestroyOnDeath)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.currentHealth = maxHealth;
                this.RpcRespawn();
            }
            
        }
        
    }

    void OnChangeHealth(int health)
    {
        this.healthBar.sizeDelta = new Vector2(health * 5, this.healthBar.sizeDelta.y);
        Debug.Log(this.healthBar.sizeDelta);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (this.isLocalPlayer)
        {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (this.NetworkStartPositions != null && this.NetworkStartPositions.Length > 0)
            {
                spawnPoint = this.NetworkStartPositions[Random.Range(0, this.NetworkStartPositions.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            this.transform.position = spawnPoint;
        }
    }
}