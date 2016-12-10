using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int initialHealth = 100;
    public int currentHealth;

    private bool isDead;

    void Awake()
    {
        this.currentHealth = this.initialHealth;
    }

    public void TakeDamage(int amount)
    {
        if (this.isDead)
            return;

        this.currentHealth -= amount;

        if (this.currentHealth <= 0)
        {
            ScoreManager.score += amount;
            this.Death();
        }
    }

    public void Death()
    {
        this.isDead = true;
    }
}
