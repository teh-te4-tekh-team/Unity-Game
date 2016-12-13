using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour
{

    public Transform player;
    public GameObject enemy;
    public float spawnTime = 3f;
    
    private int playerCount;


    void FixedUpdate()
    {
        //float distance = Vector3.Distance(this.transform.position, this.player.position);
        //float range = (GetComponent<Collider>() as SphereCollider).radius;

        PlayerController[] playerControllers = this.GetComponents<PlayerController>();

        if (playerControllers.Length == 0)
        {
            return; 
        }

        if (playerControllers.All(playerHealth => playerHealth.currentHealth <= 0))
        {
            this.CancelInvoke("CmdSpawnEnemy");
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "PlayerModel" || this.playerCount > 0) return;

        if (other.transform.root.CompareTag("Player"))
        {
            this.playerCount++;
            this.InvokeRepeating("CmdSpawnEnemy", 0f, this.spawnTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        this.playerCount--;
        this.CancelInvoke("CmdSpawnEnemy");
    }

    [Command]
    void CmdSpawnEnemy()
    {
        if (!this.isServer)
        {
            return;
        }
        GameObject spawnedEnemy = Instantiate(this.enemy, this.transform.position, this.transform.rotation) as GameObject;
        //MeshRenderer[] enemyComponents = enemy.GetComponentsInChildren<MeshRenderer>();

        /*foreach (MeshRenderer enemyComponent in enemyComponents)
        {
            enemyComponent.enabled = false;
        }*/
        NetworkServer.Spawn(spawnedEnemy);

    }

}
