using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public PlayerHealth playerHealth;

    private Vector3 movement;
    private Rigidbody playerRigidBody;

    void Awake()
    {
        this.playerRigidBody = GetComponent<Rigidbody>();
        this.playerHealth = this.GetComponent<PlayerHealth>();
    }

    void FixedUpdate()
    {
        if (this.playerHealth.currentHealth <= 0)
        {
            Die();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Move(horizontal, vertical);
    }

    public void Move(float horizontal, float vertical)
    {
        Vector3 oldPosition = this.movement;
        this.movement.Set(horizontal, 0, vertical);
        this.movement = this.movement.normalized * this.speed * Time.deltaTime;
        this.playerRigidBody.MovePosition(transform.position + movement);
        Network.OnMoveClick(oldPosition, this.movement);
    }

    public void Move(Vector3 position)
    {
        this.Move(position.x, position.z);
    } 

    void Die()
    {
        this.transform.position = Vector3.zero;
    }
}
