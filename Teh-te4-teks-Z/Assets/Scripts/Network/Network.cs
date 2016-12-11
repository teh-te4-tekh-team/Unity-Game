﻿using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour
{
    private static SocketIOComponent socket;

    public Spawner spawner;

    public GameObject currentPlayer;

    private void Start()
    {
        socket = this.GetComponent<SocketIOComponent>();

        socket.On("open", OnConnected);
        socket.On("register", OnRegister);
        socket.On("spawn", OnSpawn);
        socket.On("move", OnMove);
        socket.On("requestPosition", OnRequestPosition);
        socket.On("updatePosition", OnUpdatePosition);
        socket.On("disconnected", OnDisconnect);
    }

    private void OnConnected(SocketIOEvent e)
    {
        Debug.Log("Connected!");
    }

    private void OnSpawn(SocketIOEvent e)
    {
        GameObject player = this.spawner.SpawnPlayer(e.data["id"].str);
        if (e.data["x"])
        {
            Vector3 position = JsonUtility.FromJson<Vector3>(e.data.ToString());
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            movement.Move(position);
        }
    }

    private void OnRegister(SocketIOEvent e)
    {
        string id = e.data["id"].str;
        this.spawner.AddPlayer(id, currentPlayer);
        currentPlayer.GetComponent<NetworkEntity>().id = id;
        Debug.Log("Player with an id: " + id + " added.");
    }

    private void OnMove(SocketIOEvent e)
    {
        string playerId = e.data["id"].str;
        GameObject player = this.spawner.FindPlayer(playerId);

        Vector3 position = JsonUtility.FromJson<Vector3>(e.data.ToString());
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.Move(position);
    }

    private void OnDisconnect(SocketIOEvent e)
    {
        string id = e.data["id"].str;
        this.spawner.RemovePlayer(id);
    }

    private void OnRequestPosition(SocketIOEvent e)
    {
        socket.Emit("updatePosition", new JSONObject(JsonUtility.ToJson(this.currentPlayer.transform.position)));
    }

    private void OnUpdatePosition(SocketIOEvent e)
    {
        Debug.Log("Update position");
        string id = e.data["id"].str;
        GameObject player = this.spawner.FindPlayer(id);

        Vector3 position = JsonUtility.FromJson<Vector3>(e.data.ToString());
        player.transform.position = position;
    }

    public static void Move(Vector3 destination)
    {
        socket.Emit("move", new JSONObject(JsonUtility.ToJson(destination)));
    }
}
