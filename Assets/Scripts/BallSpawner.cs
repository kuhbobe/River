using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty = Difficulty.Hard;

    public GameObject ballPrefab;
    public Transform playerTransform;

    public float spawnDistance = 1.1f;
    public float horizontalSpreadAngle = 60f;
    public Vector2 verticalYRange = new Vector2(-0.2f, 0.2f);
    public Vector2 randomSpawnIntervalRange;

    public float spawnDuration = 60f;
    private float elapsedTime = 0f;
    private float spawnInterval;
    private float ballLifetime;
    private float timer = 0f;
    private bool lastSpawnedLeft = false;
    private bool scoreSaved = false;
    public float ElapsedTime => elapsedTime;


    public static int score = 0;

    private void Start()
    {

    }

    public void StartGame()
{
    elapsedTime = 0f;
    timer = 0f;
    score = 0;
    scoreSaved = false;
    ApplyDifficultySettings();
    ResetSpawnTimer();
}


    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime <= spawnDuration)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnBall();
                ResetSpawnTimer();
            }
        }
        else if (!scoreSaved)
        {
            PlayerPrefs.SetInt("LastScore", score);
            PlayerPrefs.Save();
            scoreSaved = true;
        }
    }

    private void ApplyDifficultySettings()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                randomSpawnIntervalRange = new Vector2(2.5f, 4f);
                ballLifetime = 6f;
                break;
            case Difficulty.Medium:
                randomSpawnIntervalRange = new Vector2(1.5f, 3f);
                ballLifetime = 4f;
                break;
            case Difficulty.Hard:
                randomSpawnIntervalRange = new Vector2(0.8f, 2f);
                ballLifetime = 2f;
                break;
        }
    }

    private void ResetSpawnTimer()
    {
        spawnInterval = Random.Range(randomSpawnIntervalRange.x, randomSpawnIntervalRange.y);
        timer = 0f;
    }

    private void SpawnBall()
    {
        Vector3 headsetPos = playerTransform.position;
        Vector3 flatForward = new Vector3(playerTransform.forward.x, 0, playerTransform.forward.z).normalized;

        float armLength = 0.8f;
        float minAngle = lastSpawnedLeft ? 10f : -horizontalSpreadAngle / 2f;
        float maxAngle = lastSpawnedLeft ? horizontalSpreadAngle / 2f : -10f;
        float angleOffset = Random.Range(minAngle, maxAngle);

        Quaternion sideRotation = Quaternion.Euler(0, angleOffset, 0);
        Vector3 spawnDirection = sideRotation * flatForward;

        Vector3 spawnPos = headsetPos + spawnDirection * armLength;
        float verticalOffset = Random.Range(verticalYRange.x, verticalYRange.y);
        spawnPos.y = headsetPos.y + verticalOffset;

        GameObject ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        Destroy(ball, ballLifetime);

        lastSpawnedLeft = !lastSpawnedLeft;
    }
}
