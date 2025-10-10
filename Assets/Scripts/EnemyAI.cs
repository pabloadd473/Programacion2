using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Solo para enemigos colocados en la escena")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] public Transform player;  // <-- jugador asignado explícitamente

    private int currentPointIndex = 0;
    private NavMeshAgent agent;
    private Attack attackComponent;

    public float detectionRange = 20f;
    private bool isInitialized = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        attackComponent = GetComponent<Attack>();

        if (attackComponent == null)
            Debug.LogError("EnemyAI no tiene componente Attack.");
    }

    void Start()
    {
        // Solo buscamos jugador si no está asignado desde el inspector o spawner
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (!isInitialized && patrolPoints != null && patrolPoints.Length > 0 && player != null)
        {
            currentPointIndex = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[currentPointIndex].position);
            isInitialized = true;
            Debug.Log($"[EnemyAI] Inicializado en Start() con {patrolPoints.Length} puntos.");
        }
        else if (!isInitialized)
        {
            Debug.LogWarning("[EnemyAI] No inicializado. Falta jugador o puntos.");
        }
    }

    void Update()
    {
        if (!isInitialized || player == null || attackComponent == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackComponent.attackRange)
            {
                attackComponent.Atacar();
            }
        }
        else
        {
            // Patrullaje
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPointIndex].position);
            }
        }
    }

    public void Initialize(Transform[] points, Transform playerTransform)
    {
        patrolPoints = points;
        player = playerTransform;

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentPointIndex = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[currentPointIndex].position);
            isInitialized = true;
            Debug.Log("[EnemyAI] Inicializado desde Spawner.");
        }
        else
        {
            Debug.LogWarning("[EnemyAI] Inicialización fallida: no hay puntos de patrulla.");
        }
    }
}
