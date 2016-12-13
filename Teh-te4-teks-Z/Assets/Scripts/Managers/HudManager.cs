using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {
    public static HudManager instance;

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
    }
}
