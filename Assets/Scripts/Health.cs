using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void RecibirDaño(int cantidad)
    {
        currentHealth -= cantidad;
        Debug.Log($"{gameObject.name} recibió {cantidad} de daño. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Morir();
        }
    }

    protected virtual void Morir()
    {
        Debug.Log($"{gameObject.name} murió.");
        // Aquí puede ir animación, efectos, etc.
    }
}
