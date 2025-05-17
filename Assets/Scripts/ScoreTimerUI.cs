using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI climbingText; 

    [Header("Game Components")]
    public BallSpawner ballSpawner; 
    public ZDistanceTracker distanceTracker;

    public Transform playerTransform; 

    private float initialZ = 0f; 

    private void Start()
    {
        if (playerTransform != null)
        {
            initialZ = playerTransform.position.y;

        }
        else
        {
            Debug.LogWarning("Player Transform not assigned in GameUIManager!");
        }
    }

    private void Update()
    {
        UpdateScore();
        UpdateTimer();
        UpdateClimbingScore();

        Debug.Log("Elapsed Time: " + distanceTracker.ElapsedTime);
    }


    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + BallSpawner.score;
        }
    }

    private void UpdateTimer()
    {
        if (timerText == null)
            return;

        float timeLeft = 0f;

        if (ballSpawner != null && ballSpawner.enabled)
        {
            timeLeft = Mathf.Max(0, ballSpawner.spawnDuration - ballSpawner.ElapsedTime);
        }
        else if (distanceTracker != null && distanceTracker.enabled)
        {
            timeLeft = Mathf.Max(0, distanceTracker.trackingDuration - distanceTracker.ElapsedTime);
        }

        timerText.text = "Time: " + timeLeft.ToString("F1") + "s";
    }


    private void UpdateClimbingScore()
    {
        if (climbingText != null && playerTransform != null)
        {
            float climbedDistance = playerTransform.position.y - initialZ;
            climbedDistance = Mathf.Max(0f, climbedDistance); 
            climbingText.text = "Climb: " + climbedDistance.ToString("F2") + "m";
        }
    }
}
