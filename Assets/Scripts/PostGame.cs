using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PostGameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject startScreenCanvas;
    public Button startButton;
    public TextMeshProUGUI startCountdownText;
    
    public GameObject postGameCanvas;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText; 
    public TextMeshProUGUI previousHighScoreText;

    [Header("Game Components")]
    public BallSpawner ballSpawner;

    [Header("Controller/Glove Objects")]
    [SerializeField] private GameObject leftControllerVisual;
    [SerializeField] private GameObject rightControllerVisual;
    [SerializeField] private GameObject leftGlove;
    [SerializeField] private GameObject rightGlove;

    private bool gameStarted = false;
    private bool gameFinished = false;

    private void Start()
    {
        ballSpawner.enabled = false;
        startScreenCanvas.SetActive(true);
        postGameCanvas.SetActive(false);
        startCountdownText.gameObject.SetActive(false);

        if (leftControllerVisual != null) leftControllerVisual.SetActive(true);
        if (rightControllerVisual != null) rightControllerVisual.SetActive(true);
        if (leftGlove != null) leftGlove.SetActive(false);
        if (rightGlove != null) rightGlove.SetActive(false);

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonPressed);
        }

      
        int previousHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (previousHighScoreText != null)
        {
            previousHighScoreText.text = "High Score: " + previousHigh;
        }
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        if (!gameFinished && ballSpawner.ElapsedTime >= ballSpawner.spawnDuration)
        {
            ShowPostGameUI();
            gameFinished = true;
        }
    }

    private void OnStartButtonPressed()
    {
        if (!gameStarted)
        {
            StartCoroutine(StartGameFlow());
        }
    }

    private IEnumerator StartGameFlow()
    {
        gameStarted = true;
        startButton.gameObject.SetActive(false);
        startCountdownText.gameObject.SetActive(true);

        if (leftControllerVisual != null) leftControllerVisual.SetActive(false);
        if (rightControllerVisual != null) rightControllerVisual.SetActive(false);
        if (leftGlove != null) leftGlove.SetActive(true);
        if (rightGlove != null) rightGlove.SetActive(true);

        string[] countdownNumbers = { "3", "2", "1", "GO!" };
        foreach (string number in countdownNumbers)
        {
            startCountdownText.text = number;
            yield return new WaitForSeconds(1f);
        }

        startCountdownText.gameObject.SetActive(false);
        startScreenCanvas.SetActive(false);

        ballSpawner.enabled = true;
        ballSpawner.StartGame();
    }

    private void ShowPostGameUI()
    {
        int finalScore = BallSpawner.score;

        int previousHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (finalScore > previousHigh)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            PlayerPrefs.Save();
        }

        scoreText.text = "Final Score: " + finalScore;
        highScoreText.text = "High Score: " + Mathf.Max(previousHigh, finalScore);

        if (leftControllerVisual != null) leftControllerVisual.SetActive(true);
        if (rightControllerVisual != null) rightControllerVisual.SetActive(true);
        if (leftGlove != null) leftGlove.SetActive(false);
        if (rightGlove != null) rightGlove.SetActive(false);

        postGameCanvas.SetActive(true);
    }
}
