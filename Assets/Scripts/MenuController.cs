using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject ExerciseBrowser;
    [SerializeField] private GameObject pauseUI;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainUIPanel;     
    [SerializeField] private GameObject settingsPanel;  

    [Header("Ball Spawner")]
    [SerializeField] private BallSpawner spawner;

    [Header("Input")]
    public InputActionProperty menuButton; 

    [Header("Controller/Glove Objects")]
    [SerializeField] private GameObject leftControllerVisual;
    [SerializeField] private GameObject rightControllerVisual;
    [SerializeField] private GameObject leftGlove;
    [SerializeField] private GameObject rightGlove;

    [Header("Gameplay Objects")]
    [SerializeField] private GameObject climbing; 

    [Header("Player")]
    [SerializeField] private Transform xrRigTransform; 


    private bool isPaused = false;

    private void OnEnable()
    {
        menuButton.action.Enable();
        menuButton.action.performed += OnMenuPressed;
    }

    private void OnDisable()
    {
        menuButton.action.performed -= OnMenuPressed;
        menuButton.action.Disable();
    }

    private void OnMenuPressed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseUI != null)
            pauseUI.SetActive(isPaused);

        if (spawner != null)
            spawner.enabled = !isPaused;

    
        if (climbing != null)
            climbing.SetActive(!isPaused); 

        
        if (leftControllerVisual != null) leftControllerVisual.SetActive(isPaused);
        if (rightControllerVisual != null) rightControllerVisual.SetActive(isPaused);
        if (leftGlove != null) leftGlove.SetActive(!isPaused);
        if (rightGlove != null) rightGlove.SetActive(!isPaused);

      
        if (!isPaused)
        {
            if (mainUIPanel != null) mainUIPanel.SetActive(true);    
            if (settingsPanel != null) settingsPanel.SetActive(false); 
        }

        if (xrRigTransform != null)
        {
            xrRigTransform.position = Vector3.zero;
            xrRigTransform.rotation = Quaternion.identity;
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    // === Main Menu UI Functions ===
    public void ShowLevels(int world)
    {
        GameManager.Instance.currentWorld = world;
        if (ExerciseBrowser != null)
            ExerciseBrowser.SetActive(true);
    }

    public void MainScreen()
    {
        SceneManager.LoadScene("Start");
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
