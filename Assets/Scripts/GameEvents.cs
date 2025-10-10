using System;
using UnityEngine;

public static class GameEvents

{
    // EVENTO 1: Sedispara cuando un jugador recolecta un fragmento de energia
    // Utiliza 'Action' porque no requiere pasar argumentos.

    public static event Action OnFragmentCollected;

    // Metodo estatico para que los scripts llamen al evento (el Publicador),
    public static void FragmentCollected()
    {
        // el operador ? Invoke()  asegura que solo se invoque si hay suscriptores (null check).
        OnFragmentCollected?.Invoke();
        Debug.Log("EVENTO LANZADO: Fragmento recolectado, Notificando a los sistemas...");
    }

    // Ejemplo de Evento que podria ser util

    // EVENTO 2: Se dispara cuando la salud del jugador cambia, pasando el nuevo valor de salud.

    public static event Action<float> OnHealthChanged;

    public static void HealthChanged(float newHealth)
    {
        OnHealthChanged?.Invoke(newHealth);
    }
}