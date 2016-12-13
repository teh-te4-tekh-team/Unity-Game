using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    
    GameObject[] players;
    NavMeshAgent nav;    

    void Awake()
    {
        this.players = GameObject.FindGameObjectsWithTag("Player");
        this.nav = this.GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        foreach (GameObject player in this.players)
        {
            if (player.GetComponent<PlayerController>().currentHealth <= 0)
            {
                this.nav.Stop();
            }

            this.nav.SetDestination(player.transform.position);
            this.nav.speed = 30;
        }
             
    }
}
