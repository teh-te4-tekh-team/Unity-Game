using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public PlayerHealth playerHealth;
    

    Transform player;
    NavMeshAgent nav;    

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.playerHealth = this.player.GetComponent<PlayerHealth>();
        this.nav = this.GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if (this.playerHealth.currentHealth <= 0)
        {
            this.nav.Stop();
        }

        this.nav.SetDestination(this.player.position);
        this.nav.speed = 30;        
    }
}
