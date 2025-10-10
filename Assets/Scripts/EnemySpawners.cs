using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración del spawn")]
    public GameObject enemigoPrefab;
    public Vector3 areaCentro;
    public Vector3 areaTamaño = new Vector3(70, 0, 70);
    public float intervalo = 30f;
    public Transform[] patrolPoints; // Puntos de patrulla para asignar a cada enemigo

    private float tiempoSiguienteSpawn = 0f;
    private Transform jugador;

    [Header("Límite de enemigos")]
    public int maxEnemigos = 10;
    private List<GameObject> enemigosActivos = new List<GameObject>();

    void Start()
    {
        tiempoSiguienteSpawn = Time.time + intervalo;
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (patrolPoints == null || patrolPoints.Length == 0)
            Debug.LogWarning("No hay puntos de patrulla asignados en EnemySpawner!");
        if (jugador == null)
            Debug.LogWarning("No se encontró el jugador con tag 'Player' en EnemySpawner!");
        if (enemigoPrefab == null)
            Debug.LogError("No se ha asignado el prefab de enemigo en EnemySpawner!");
    }

    void Update()
    {
        // Limpiar enemigos destruidos de la lista
        enemigosActivos.RemoveAll(e => e == null);

        if (!UIManager.Win && Time.time >= tiempoSiguienteSpawn)
        {
            if (enemigosActivos.Count < maxEnemigos)
            {
                SpawnEnemigo();
            }

            tiempoSiguienteSpawn = Time.time + intervalo;
        }
    }

    void SpawnEnemigo()
    {
        if (enemigoPrefab == null)
        {
            Debug.LogError("Prefab enemigo no asignado, no se puede spawnear.");
            return;
        }
        if (jugador == null)
        {
            Debug.LogWarning("No hay jugador asignado, no se puede inicializar enemigo.");
            return;
        }
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogWarning("No hay puntos de patrulla, enemigo no inicializado correctamente.");
            return;
        }

        Vector3 spawnPos = areaCentro + new Vector3(
            Random.Range(-areaTamaño.x / 2, areaTamaño.x / 2),
            0,
            Random.Range(-areaTamaño.z / 2, areaTamaño.z / 2)
        );

        GameObject nuevoEnemigo = Instantiate(enemigoPrefab, spawnPos, Quaternion.identity);
        enemigosActivos.Add(nuevoEnemigo); // 👈 Lo agregamos a la lista

        EnemyAI patrol = nuevoEnemigo.GetComponent<EnemyAI>();

        if (patrol == null)
        {
            Debug.LogError("El prefab de enemigo NO tiene componente EnemyAI!");
            Destroy(nuevoEnemigo);
            return;
        }

        patrol.Initialize(patrolPoints, jugador);

        Debug.Log($"Enemigo clonado en {spawnPos}. Enemigos activos: {enemigosActivos.Count}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(areaCentro, areaTamaño);
    }
}
