using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health; // Referencia al script de salud
    public Slider slider; // El slider UI

    void Start()
    {
        if (health == null)
        {
            Debug.LogError("No se asignó Health al HealthBar");
            enabled = false;
            return;
        }

        slider.maxValue = health.maxHealth;
        slider.value = health.currentHealth;
    }

    void Update()
    {
        slider.value = health.currentHealth;
    }
}
