using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GanarTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Ganaste!");
            UIManager.Inst.ShowWinScreen();
        }
    }
}
