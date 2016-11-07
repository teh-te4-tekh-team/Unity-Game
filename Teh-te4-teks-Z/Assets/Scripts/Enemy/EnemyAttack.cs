using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyMovement enemyMovement;
    private bool playerInRange;
    private float timer;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerHealth = this.player.GetComponent<PlayerHealth>();
        this.enemyHealth = GetComponent<EnemyHealth>();
        this.enemyMovement = GetComponent<EnemyMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.player)
        {
            this.playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == this.player)
        {
            this.playerInRange = false;
        }
    }

    void FixedUpdate()
    {
        this.timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && 
            playerInRange &&
            this.enemyHealth.currentHealth > 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        this.timer = 0f;

        if (this.playerHealth.currentHealth > 0)
        {
            this.playerHealth.TakeDamage(attackDamage);
        }
        else
        {
            enemyMovement.enabled = false;
        }
    }
}
