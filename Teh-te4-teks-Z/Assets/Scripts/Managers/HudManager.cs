using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {
    public static HudManager instance;

    public Animator Animator;

    private Text healthText;
    private Slider healthSlider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            this.healthText = GameObject.Find("HealthText").GetComponent<Text>();
            this.healthSlider = this.GetComponentInChildren<Slider>();
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void UpdateHealth(int health)
    {
        this.healthText.text = string.Format("{0}", health);
        this.healthSlider.value = health;

        if (health <= 0)
        {
            this.Animator.SetTrigger("PlayerDead");
        }
    }
}
