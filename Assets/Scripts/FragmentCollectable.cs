using UnityEngine;

public class FragmentCollectable : MonoBehaviour
{
    [Tooltip("El Tag que identifica al jugador")]

    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        //Verifica si el objeto que colisiono es el jugador
        if (other.CompareTag(playerTag))
        {
            // 1. Lanza el evento Global, avisa al resto del juego.
            GameEvents.FragmentCollected();
            // 2. Ejecuta la acción local: destruye el objeto.
            Destroy(gameObject);

        }
    }
}