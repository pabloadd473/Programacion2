using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolBase : MonoBehaviour
{
    protected Transform[] patrolPoints;
    protected NavMeshAgent agent;
    protected int currentPoint = 0;
    protected bool isInitialized = false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Asignar patrolPoints autom�ticamente, si no fueron asignados en inspector ni por c�digo
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            GameObject puntosPadre = GameObject.Find("PatrolPointsParent"); // ejemplo
            if (puntosPadre != null)
            {
                patrolPoints = puntosPadre.GetComponentsInChildren<Transform>();
            }
            else
            {
                Debug.LogWarning($"{gameObject.name}: No se encontr� objeto PatrolPointsParent para asignar patrolPoints.");
            }
        }

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentPoint = 0;
            agent.destination = patrolPoints[currentPoint].position;
            isInitialized = true;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} no tiene patrolPoints asignados en EnemyPatrolBase.");
        }
    }
}
