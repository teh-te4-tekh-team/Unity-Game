using UnityEngine;

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
            this.CancelInvoke("SpawnEnemy");
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name != "CurrentPlayer") return;
        
        if (other.transform.root.CompareTag("Player"))
        {
            this.InvokeRepeating("SpawnEnemy", 0f, this.spawnTime);
        }        
    }

    void OnTriggerExit(Collider other)
    {
        this.CancelInvoke("SpawnEnemy");
    }

    void SpawnEnemy()
    {
        Instantiate(this.enemy, this.transform.position, this.transform.rotation);
        this.enemy.GetComponentInChildren<MeshRenderer>().enabled = false;
    }
}
