using UnityEngine;

public class SystemListener : MonoBehaviour
{
    [Header("Configuraci�n de Sistema")]
    [Tooltip("La Funcionalidad de este Listener (ej. 'Actualizar UI', Reproducir SFX').")]
    public string listenerFunction = "Actualizar Contador";

    // Un AudioSource para demostrar la reacci�n de audio.
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // -----SUSCRIPCI�N----
        GameEvents.OnFragmentCollected += OnFragmentEvent;
        Debug.Log($"Sistema{listenerFunction} SUSCRIPTO al evento.");
    }

    void OnDestroy()
    {
        // --- DESUSCRIPCI�N -----
        GameEvents.OnFragmentCollected -= OnFragmentEvent;
    }

    //Este m�todo se ejecuta autom�ticamente cuando el evento OnFragmentCollected es Lanzado
    private void OnFragmentEvent()
    {
        Debug.Log($"[Listener: {listenerFunction}] �Evento recibido, ejecutando l�gica...");
        if (audioSource == null)
        {
            Debug.LogWarning($"[SystemListener: {listenerFunction}] No se encontr� AudioSource en el GameObject.");
        }


        //L�gica de reacci�n
        if (audioSource != null)
        {
            //Reproducir un Sonido (si existe audiosource)
            audioSource.Play();
        }
        //Simula la actualizaci�n de la interfaz o la l�gica de la misi�n.
        // Aqu� se actualizar�a el HUD de un juego top-down.
        Debug.Log("Contador de Fragmentos actualizado");
    } 

    

}