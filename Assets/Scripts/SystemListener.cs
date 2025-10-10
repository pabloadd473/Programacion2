using UnityEngine;

public class SystemListener : MonoBehaviour
{
    [Header("Configuración de Sistema")]
    [Tooltip("La Funcionalidad de este Listener (ej. 'Actualizar UI', Reproducir SFX').")]
    public string listenerFunction = "Actualizar Contador";

    // Un AudioSource para demostrar la reacción de audio.
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // -----SUSCRIPCIÓN----
        GameEvents.OnFragmentCollected += OnFragmentEvent;
        Debug.Log($"Sistema{listenerFunction} SUSCRIPTO al evento.");
    }

    void OnDestroy()
    {
        // --- DESUSCRIPCIÓN -----
        GameEvents.OnFragmentCollected -= OnFragmentEvent;
    }

    //Este método se ejecuta automáticamente cuando el evento OnFragmentCollected es Lanzado
    private void OnFragmentEvent()
    {
        Debug.Log($"[Listener: {listenerFunction}] ¡Evento recibido, ejecutando lógica...");
        if (audioSource == null)
        {
            Debug.LogWarning($"[SystemListener: {listenerFunction}] No se encontró AudioSource en el GameObject.");
        }


        //Lógica de reacción
        if (audioSource != null)
        {
            //Reproducir un Sonido (si existe audiosource)
            audioSource.Play();
        }
        //Simula la actualización de la interfaz o la lógica de la misión.
        // Aquí se actualizaría el HUD de un juego top-down.
        Debug.Log("Contador de Fragmentos actualizado");
    } 

    

}