using UnityEngine;
using TMPro;

public class PostGameUIManager : MonoBehaviour
{
    public GameObject postGameCanvas;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI highScoreText;

    public BallSpawner ballSpawner;

    [Header("Controller/Glove Objects")]
    [SerializeField] private GameObject leftControllerVisual;
    [SerializeField] private GameObject rightControllerVisual;
    [SerializeField] private GameObject leftGlove;
    [SerializeField] private GameObject rightGlove;

    private bool shown = false;

    void Update()
    {
        if (!shown && ballSpawner.ElapsedTime >= ballSpawner.spawnDuration)
        {
            ShowPostGameUI();
            shown = true;
        }
    }

    void ShowPostGameUI()
    {
        int finalScore = BallSpawner.score;

        // Update high score
        int previousHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (finalScore > previousHigh)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            PlayerPrefs.Save();
        }

        // Fill out UI fields
        scoreText.text = "Final Score: " + finalScore;
        difficultyText.text = "Difficulty: " + ballSpawner.currentDifficulty;
        highScoreText.text = "High Score: " + Mathf.Max(previousHigh, finalScore);

        // Show controllers, hide gloves
        if (leftControllerVisual != null) leftControllerVisual.SetActive(true);
        if (rightControllerVisual != null) rightControllerVisual.SetActive(true);
        if (leftGlove != null) leftGlove.SetActive(false);
        if (rightGlove != null) rightGlove.SetActive(false);

        // Show the post-game UI
        postGameCanvas.SetActive(true);
    }
}
