using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    private PlayerHealth playerHealth;

    private Vector3 movement;
    private Rigidbody playerRigidBody;

    private Animator animator;

    private string id;

    private string currentPlayerId;

    private void Awake()
    {
        this.playerRigidBody = this.GetComponent<Rigidbody>();
        this.playerHealth = this.GetComponent<PlayerHealth>();
        this.animator = this.GetComponentInChildren<Animator>();
    }

    public string PlayerId
    {
        get { return this.currentPlayerId; }
        set { this.currentPlayerId = value; }
    }

    public string Id
    {
        get { return this.id; }
        set { this.id = value; }
    }

    void FixedUpdate()
    {
        if (this.playerHealth.currentHealth <= 0)
        {
            Die();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (IsCurrentPlayer())
        {
            Move(horizontal, vertical);
        }
    }

    private void Move(float horizontal, float vertical)
    {
        bool isWalking = vertical != 0 || horizontal != 0;
        this.animator.SetBool("IsWalking", isWalking);
        if (isWalking)
        {
            this.movement.Set(horizontal, 0, vertical);
            this.movement = this.movement.normalized * this.speed * Time.deltaTime;
            this.playerRigidBody.MovePosition(transform.position + movement);

            Network.Move(transform.position + movement);
        }
    }

    private void Die()
    {
        this.transform.position = Vector3.zero;
    }

    private bool IsCurrentPlayer()
    {
        return this.Id == this.PlayerId;
    }
}
