using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int initialHealth = 100;
    public int currentHealth;

    void Awake()
    {
        this.currentHealth = this.initialHealth;
    }
}
