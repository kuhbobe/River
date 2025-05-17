using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ZPostGameUIManager : MonoBehaviour
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
    public ZDistanceTracker distanceTracker;
    public Transform playerTransform;

    public GameObject climbingWall;

    private bool gameStarted = false;
    private bool gameFinished = false;

    private void Start()
    {
        distanceTracker.enabled = false;
        startScreenCanvas.SetActive(true);
        postGameCanvas.SetActive(false);
        startCountdownText.gameObject.SetActive(false);

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonPressed);
        }

        // Show previous high score
        float previousHigh = PlayerPrefs.GetFloat("ZHighScore", 0f);
        if (previousHighScoreText != null)
        {
            previousHighScoreText.text = "High Score: " + Mathf.FloorToInt(previousHigh);
        }
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        if (!gameFinished && distanceTracker.ElapsedTime >= distanceTracker.trackingDuration)
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

        string[] countdownNumbers = { "3", "2", "1", "GO!" };
        foreach (string number in countdownNumbers)
        {
            startCountdownText.text = number;
            yield return new WaitForSeconds(1f);
        }

        startCountdownText.gameObject.SetActive(false);
        startScreenCanvas.SetActive(false);

        distanceTracker.enabled = true;
        climbingWall.SetActive(true);
        distanceTracker.StartTracking();
    }

    private void ShowPostGameUI()
    {
        float finalScore = ZDistanceTracker.finalZScore;


        float previousHigh = PlayerPrefs.GetFloat("ZHighScore", 0f);
        if (finalScore > previousHigh)
        {
            PlayerPrefs.SetFloat("ZHighScore", finalScore);
            PlayerPrefs.Save();
        }

        scoreText.text = "Final Score: " + Mathf.FloorToInt(finalScore);
        highScoreText.text = "High Score: " + Mathf.FloorToInt(Mathf.Max(previousHigh, finalScore));

    
       
        if (playerTransform != null)
        {
            playerTransform.position = Vector3.zero;
        }

        postGameCanvas.SetActive(true);
        climbingWall.SetActive(false);
    }
}
