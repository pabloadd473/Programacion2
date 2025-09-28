using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 20;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public bool PantallaGanar = false;
    private float lastAttackTime = 0f;

    public LayerMask targetLayer; // Configurar: jugador pone "Enemigos", enemigo pone "Jugador"

    void Update()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Para jugador: detectar input de ataque (clic izquierdo)
            if (gameObject.CompareTag("Player") && Input.GetButtonDown("Fire1"))
            {
                Atacar();
            }
            if (!gameObject.CompareTag("Player"))
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
                if (hits.Length > 0)
                {
                    Atacar();
                }
            }
            // Para enemigo: atacar automáticamente si jugador está en rango
           
        }        
    }
  


    void Atacar()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, targetLayer);

        foreach (var hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.RecibirDaño(damage);
                Debug.Log($"{gameObject.name} atacó a {hit.name} por {damage} puntos de daño.");
            }
        }

        lastAttackTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
