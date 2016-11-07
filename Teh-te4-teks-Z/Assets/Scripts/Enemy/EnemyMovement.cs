using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    Transform player;
    NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        nav.SetDestination(player.position);
    }
}
