using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPoint = 0;

    public Transform jugador;

    public float distanciaDeteccion = 10f;
    public float distanciaAtaque = 2f;

    public float tiempoEntreAtaques = 1.5f;
    private float timerAtaque = 0f;

    private EnemyHealth enemyHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        if (patrolPoints.Length > 0)
            agent.destination = patrolPoints[0].position;

        if (jugador == null)
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enemyHealth.currentHealth <= 0) return; // No hacer nada si está muerto

        timerAtaque += Time.deltaTime;

        if (jugador == null)
            return;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= distanciaDeteccion)
        {
            agent.SetDestination(jugador.position);

            if (distanciaAlJugador <= distanciaAtaque)
            {
                if (timerAtaque >= tiempoEntreAtaques)
                {
                    AtacarJugador();
                    timerAtaque = 0f;
                }
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = false;

            if (patrolPoints.Length == 0 || agent.pathPending)
                return;

            if (agent.remainingDistance < 0.5f)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                agent.destination = patrolPoints[currentPoint].position;
            }
        }
    }

    void AtacarJugador()
    {
        IDamageable damageableJugador = jugador.GetComponent<IDamageable>();
        if (damageableJugador != null)
        {
            int daño = 10;
            damageableJugador.RecibirDaño(daño);
            Debug.Log($"{gameObject.name} atacó al jugador causando {daño} de daño.");
        }
    }
}
