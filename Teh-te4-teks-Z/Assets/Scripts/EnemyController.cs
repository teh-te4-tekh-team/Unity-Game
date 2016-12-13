using UnityEngine;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour
{

    public int initialHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth;

    public RectTransform healthBar;

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerController playerController;
    private int playerHealth;
    private bool playerInRange;
    private float timer;

    private NavMeshAgent nav;

    void OnChangeHealth(int health)
    {
        this.healthBar.sizeDelta = new Vector2(health * 5, this.healthBar.sizeDelta.y);
    }

    void Awake()
    {
        this.currentHealth = this.initialHealth;
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerController = this.player.GetComponent<PlayerController>();
        this.playerHealth = this.playerController.currentHealth;
        this.nav = this.GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if (this.playerHealth <= 0)
        {
            this.nav.Stop();
        }

        this.nav.SetDestination(this.player.transform.position);
        this.nav.speed = 15;

        this.timer += Time.deltaTime;

        if (this.timer >= this.timeBetweenAttacks
            && this.playerInRange
            && this.currentHealth > 0)
        {
            this.Attack();
        }
    }

    public void TakeDamage(int amount)
    {
        this.currentHealth -= amount;

        if (this.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
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

    void Attack()
    {
        this.timer = 0f;

        if (this.playerController.currentHealth > 0)
        {
            this.playerController.TakeDamage(this.attackDamage);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
