using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty = Difficulty.Medium;

    public GameObject ballPrefab;
    public Transform playerTransform;

    public float spawnDistance = 1.1f; // closer for arm-length reach

    public float horizontalSpreadAngle = 60f; // total cone angle
    public Vector2 verticalYRange = new Vector2(-0.2f, 0.2f);
    public Vector2 randomSpawnIntervalRange = new Vector2(1.0f, 3.0f);

    private float spawnInterval;
    private float ballLifetime;
    private float timer = 0f;
    private bool lastSpawnedLeft = false; // tracks last spawn side
    

    private void Start()
    {
        ApplyDifficultySettings();
        ResetSpawnTimer();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBall();
            ResetSpawnTimer();
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

    // Flatten forward to avoid head tilt issues
    Vector3 flatForward = new Vector3(playerTransform.forward.x, 0, playerTransform.forward.z).normalized;

    float armLength = .8f; // comfortable reach

    // Angle bias: stronger direction toward left or right depending on last spawn
    float minAngle = lastSpawnedLeft ? 10f : -horizontalSpreadAngle / 2f;
    float maxAngle = lastSpawnedLeft ? horizontalSpreadAngle / 2f : -10f;
    float angleOffset = Random.Range(minAngle, maxAngle);

    Quaternion sideRotation = Quaternion.Euler(0, angleOffset, 0);
    Vector3 spawnDirection = sideRotation * flatForward;

    Vector3 spawnPos = headsetPos + spawnDirection * armLength;

    // Slight vertical variation
    float verticalOffset = Random.Range(verticalYRange.x, verticalYRange.y);
    spawnPos.y = headsetPos.y + verticalOffset;

    GameObject ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    Destroy(ball, ballLifetime);

    // Alternate glove side for next spawn
    lastSpawnedLeft = !lastSpawnedLeft;
}


}
