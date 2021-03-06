﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public PlayerHealth playerHealth;

    public GameUser CurrentUser;

    private Vector3 movement;
    private Rigidbody playerRigidBody;

    private MainMenu menu;

    void Awake()
    {
        this.playerRigidBody = this.GetComponent<Rigidbody>();
        this.playerHealth = this.GetComponent<PlayerHealth>();
        this.CurrentUser = new GameUser();
    }

    void FixedUpdate()
    {
        if (this.playerHealth.currentHealth <= 0)
        {
            this.Dead();
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        this.Move(horizontal, vertical);
    }

    void Move(float horizontal, float vertical)
    {
        this.movement.Set(horizontal, 0, vertical);
        this.movement = this.movement.normalized * this.speed * Time.deltaTime;
        this.playerRigidBody.MovePosition(this.transform.position + this.movement);
    }

    void Dead()
    {
        this.transform.position = Vector3.zero;
    }
}
