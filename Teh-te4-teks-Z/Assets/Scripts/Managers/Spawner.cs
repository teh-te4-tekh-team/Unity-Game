﻿using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static readonly Vector3 InitialPosition = new Vector3(50, 2.5f, 10);

    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    /// <summary>
    /// Player prefab used to instantiate new players.
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// Reference to the currently connected player (the player in the scene).
    /// </summary>
    public GameObject currentPlayer;

    /// <summary>
    /// Adds new player to the current game.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject SpawnPlayer(string id)
    {
        // Create an instance of an object passed as Component.
        GameObject player = Instantiate(this.playerPrefab, InitialPosition, Quaternion.identity) as GameObject;
        player.GetComponent<NetworkEntity>().id = id;
        string playerId = this.currentPlayer.GetComponent<NetworkEntity>().id;
        player.GetComponent<PlayerMovement>().PlayerId = playerId;
        player.GetComponent<PlayerMovement>().Id = id;

        this.AddPlayer(id, player);

        return player;
    }

    /// <summary>
    /// Adds player reference.
    /// </summary>
    /// <param name="id">Player's id.</param>
    /// <param name="player">Game object with which the player will be assosiated with.</param>
    public void AddPlayer(string id, GameObject player)
    {
        this.players.Add(id, player);
    }

    public GameObject FindPlayer(string id)
    {
        return this.players[id];
    }

    public void RemovePlayer(string id)
    {
        GameObject player = this.players[id];
        Destroy(player);
        this.players.Remove(id);
    }
}
