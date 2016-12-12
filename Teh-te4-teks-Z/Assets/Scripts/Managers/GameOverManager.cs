using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
    public PlayerHealth playerHealth;

    Animator animator;

	void Awake ()
    {
        animator = GetComponent<Animator>();    
	}
	
	void Update ()
    {
        if (playerHealth.currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
        }
    }
}
