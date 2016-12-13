using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerController playerController;
    EnemyHealth enemyHealth;
    EnemyMovement enemyMovement;
    private bool playerInRange;
    private float timer;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerController = this.player.GetComponent<PlayerController>();
        this.enemyHealth = GetComponent<EnemyHealth>();
        this.enemyMovement = GetComponent<EnemyMovement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.root.tag == "Player")
        {
            this.playerInRange = true;
        }       
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.root.tag == "Player")
        {
            this.playerInRange = false;
        }
    }

    void FixedUpdate()
    {
        this.timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks 
            && playerInRange 
            && this.enemyHealth.currentHealth > 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        this.timer = 0f;

        if (this.playerController.currentHealth > 0)
        {
            this.playerController.TakeDamage(attackDamage);
        }
        else
        {
            enemyMovement.enabled = false;
        }
    }
}
