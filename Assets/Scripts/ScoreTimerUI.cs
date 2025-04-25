using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public BallSpawner ballSpawner; // Reference to your BallSpawner

    private void Update()
    {
        // Update score display
        scoreText.text = "Score: " + BallSpawner.score;

        // Calculate time remaining
        float timeLeft = Mathf.Max(0, ballSpawner.spawnDuration - ballSpawner.ElapsedTime);
        timerText.text = "Time: " + timeLeft.ToString("F1") + "s";
    }
}
