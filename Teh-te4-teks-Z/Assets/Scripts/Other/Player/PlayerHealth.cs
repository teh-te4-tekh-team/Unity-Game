using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int InitialHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Text healthText;

    PlayerMovement playerMovement;
    bool isDead = false;

    void Awake()
    {
        this.currentHealth = this.InitialHealth;
        this.playerMovement = GetComponent<PlayerMovement>();
    }
    
    public void TakeDamage(int amount)
    {
        this.currentHealth -= amount;
        this.healthSlider.value = this.currentHealth;
        this.healthText.text = this.currentHealth.ToString();

        if (this.currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        playerMovement.enabled = false;
    }
}
