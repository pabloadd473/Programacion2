using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Inst;

    public TextMeshProUGUI TimeCounterGamePlay;
    public TextMeshProUGUI TimeCounterWin;
    public float TimesSeconds = 0;
    public int TimeMinutes = 0;

    public Button RetryButton;
    public Button ContinueButton;
    public Button MainMenuButton;
    public Button MainMenuButtonPause;
    public Button MainMenuButtonDerrota;
    public Button NextSceneButton;

    private bool PantallaGanar = false;
    public bool Pause;

    public GameObject WinScreen;
    public GameObject PauseScreen;
    public GameObject ExitScreen;

    public static bool Win;
    public int vida = 1;

    private void Awake()
    {
        Inst = this;

        RetryButton.onClick.AddListener(OnRetryButton);
        ContinueButton.onClick.AddListener(OnContinueButton);
        MainMenuButton.onClick.AddListener(OnMainMenuButton);
        MainMenuButtonPause.onClick.AddListener(OnMainMenuButton);
        MainMenuButtonDerrota.onClick.AddListener(OnMainMenuButton);
        NextSceneButton.onClick.AddListener(OnNextSceneButton);
        ResumeGame();
    }

    void Update()
    {
        // Detecta Escape para pausar/reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            Debug.Log("entro la tecla esc");
        }

        if (!Win && !Pause)
        {
            TimesSeconds += Time.deltaTime;
            if (TimesSeconds >= 59)
            {
                TimeMinutes++;
                TimesSeconds = 0;
            }
            TimeCounterGamePlay.gameObject.SetActive(true);
            if ((TimeMinutes < 10) && (TimesSeconds < 9))
            {
                TimeCounterGamePlay.text = "Tiempo: 0" + TimeMinutes + ":0" + Mathf.Ceil(TimesSeconds);
                TimeCounterWin.text = "Tiempo: 0" + TimeMinutes + ":0" + Mathf.Ceil(TimesSeconds);
            }
            else
            {
                if (TimeMinutes < 10)
                {
                    TimeCounterGamePlay.text = "Tiempo: 0" + TimeMinutes + ":" + Mathf.Ceil(TimesSeconds);
                    TimeCounterWin.text = "Tiempo: 0" + TimeMinutes + ":" + Mathf.Ceil(TimesSeconds);
                }
                else
                {
                    if (TimesSeconds < 9)
                    {
                        TimeCounterGamePlay.text = "Tiempo: " + TimeMinutes + ":0" + Mathf.Ceil(TimesSeconds);
                        TimeCounterWin.text = "Tiempo: " + TimeMinutes + ":0" + Mathf.Ceil(TimesSeconds);
                    }
                    else
                    {
                        TimeCounterGamePlay.text = "Tiempo: " + TimeMinutes + ":" + Mathf.Ceil(TimesSeconds);
                        TimeCounterWin.text = "Tiempo: " + TimeMinutes + ":" + Mathf.Ceil(TimesSeconds);
                    }
                    }
            }
            }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Tag del objeto: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ganar"))
        {

            Debug.Log("entre en Colision");
            Win = true;
        }
        else Win = false;


        Debug.Log("Preguntando en Colision");
        if (Win && !PantallaGanar)
        {
            UIManager.Inst.ShowWinScreen();
            PantallaGanar = true;
        }
    }



    // Lógica para mostrar pantallas
    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
        Win = true;
        TimeCounterGamePlay.gameObject.SetActive(false);
        TimeCounterWin.gameObject.SetActive(true);
        Debug.Log("Mostrando pantalla de victoria");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pause = true;

        Time.timeScale = 0f; // Congela el juego al ganar
        DisablePlayerControls();
    }

    public void ShowPauseScreen()
    {
        PauseScreen.SetActive(true);
        TimeCounterGamePlay.gameObject.SetActive(false);
        Debug.Log("Mostrando pantalla de pausa");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Pause = true;
        Time.timeScale = 0f;
        DisablePlayerControls();
    }

    public void ShowDerrotaScreen()
    {
        ExitScreen.SetActive(true);
        Debug.Log("Mostrando pantalla de derrota");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Pause = true;
        Time.timeScale = 0f;
        DisablePlayerControls();
    }

    // Manejo de pausa
    public void TogglePause()
    {
        if (Pause)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        Pause = true;
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
        TimeCounterGamePlay.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisablePlayerControls();
    }

    private void ResumeGame()
    {
        Pause = false;
        Win = false;    
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        TimeCounterGamePlay.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EnablePlayerControls();
    }

    private void DisablePlayerControls()
    {
        var input = FindObjectOfType<StarterAssetsInputs>();
        if (input != null)
        {
            input.cursorLocked = false;
            input.cursorInputForLook = false;
            input.enabled = false;
        }
        var controller = FindObjectOfType<ThirdPersonController>();
        if (controller != null)
            controller.enabled = false;
    }

    private void EnablePlayerControls()
    {
        var input = FindObjectOfType<StarterAssetsInputs>();
        if (input != null)
        {
            input.cursorLocked = true;
            input.cursorInputForLook = true;
            input.enabled = true;
        }
        var controller = FindObjectOfType<ThirdPersonController>();
        if (controller != null)
            controller.enabled = true;
    }

    // Botones
    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Pause = false;
    }

    public void OnContinueButton()
    {
        ResumeGame();
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Pause = false;
        Win = false;
    }
    public void OnNextSceneButton()
    {
    Time.timeScale = 1f;
        Debug.Log("Next Level");
    SceneManager.LoadScene(2);
    Pause = false;
    Win = false;
    }
}
