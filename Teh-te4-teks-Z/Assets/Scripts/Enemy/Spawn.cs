using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public Transform player;
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;


    void FixedUpdate()
    {
        //float distance = Vector3.Distance(this.transform.position, this.player.position);
        //float range = (GetComponent<Collider>() as SphereCollider).radius;

        if (this.playerHealth.currentHealth <= 0)
        {
            CancelInvoke("SpawnEnemy");
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnTime);
        }        
    }

    void OnTriggerExit(Collider other)
    {
        CancelInvoke("SpawnEnemy");
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, this.transform.position, this.transform.rotation);
    }
}
