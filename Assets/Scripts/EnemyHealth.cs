using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Morir()
    {
       // EnemySpawner.Instance?.NotificarMuerte(gameObject);
       
        Debug.Log($"{gameObject.name} enemigo muri�.");
        Destroy(gameObject);
    }
}
