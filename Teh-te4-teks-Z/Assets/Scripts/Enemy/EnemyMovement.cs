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
            nav.Stop();
        }

        nav.SetDestination(player.position);
        nav.speed = 30;        
    }
}
