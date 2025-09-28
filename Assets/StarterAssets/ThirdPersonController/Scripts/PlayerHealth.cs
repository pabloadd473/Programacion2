using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private ThirdPersonController playerController;
    private StarterAssets.StarterAssetsInputs playerInput;

    private void Start()
    {
        currentHealth = 300;
        maxHealth = 300;

        playerController = GetComponent<ThirdPersonController>();
        playerInput = GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    protected override void Morir()
    {
        Debug.Log("Jugador murió. Game Over o reiniciar escena.");

        if (playerController != null) playerController.enabled = false;
        if (playerInput != null)
        {
            playerInput.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (UIManager.Inst != null)
        {
            UIManager.Inst.ShowDerrotaScreen();
        }
    }

    void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
