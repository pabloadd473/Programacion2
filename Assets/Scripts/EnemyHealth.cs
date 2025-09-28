using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Morir()
    {
        Debug.Log($"{gameObject.name} enemigo murió.");
        Destroy(gameObject);
    }
}
