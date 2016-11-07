using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;

    private Vector3 movement;
    private Rigidbody playerRigidBody;

    void Awake()
    {
        this.playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Move(horizontal, vertical);
    }

    void Move(float horizontal, float vertical)
    {
        this.movement.Set(horizontal, 0, vertical);
        this.movement = this.movement.normalized * this.speed * Time.deltaTime;
        this.playerRigidBody.MovePosition(transform.position + movement);
    }
}
