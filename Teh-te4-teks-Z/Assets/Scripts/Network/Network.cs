using UnityEngine;
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
        socket.On("attack", OnAttack);
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
        Debug.Log("Register");
        string id = e.data["id"].str;
        this.spawner.AddPlayer(id, currentPlayer);
    }

    private void OnMove(SocketIOEvent e)
    {
        string playerId = e.data["id"].str;
        GameObject player = this.spawner.FindPlayer(playerId);

        Vector3 position = JsonUtility.FromJson<Vector3>(e.data.ToString());
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.Move(position);
    }

    private void OnAttack(SocketIOEvent e)
    {
        string playerId = e.data["id"].str;
        GameObject player = this.spawner.FindPlayer(playerId);
        player.GetComponent<Animator>().SetTrigger("Attack");

        string targetId = e.data["targetId"].str;
        GameObject target = this.spawner.FindPlayer(targetId);
        target.GetComponent<PlayerHealth>().TakeDamage(10);
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
        string id = e.data["id"].str;
        GameObject player = this.spawner.FindPlayer(id);

        Vector3 position = JsonUtility.FromJson<Vector3>(e.data.ToString());
        player.transform.position = position;
    }

    public static void OnMoveClick(Vector3 current, Vector3 destination)
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("c", JsonUtility.ToJson(current));
        json.AddField("d", JsonUtility.ToJson(destination));

        socket.Emit("move", json);
    }
}
