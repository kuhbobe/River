using UnityEngine;

public class ZDistanceTracker : MonoBehaviour
{
    public Transform playerTransform;

    public float trackingDuration = 60f;
    private float elapsedTime = 0f;
    private bool scoreSaved = false;

    public float ElapsedTime => elapsedTime;

    public static float finalZScore = 0f;

    private void Start()
    {

        enabled = false;
    }

    public void StartTracking()
    {
        elapsedTime = 0f;
        finalZScore = 0f;
        scoreSaved = false;
        enabled = true;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime <= trackingDuration)
        {
            finalZScore = playerTransform.position.y;

        }
        else if (!scoreSaved)
        {
            PlayerPrefs.SetFloat("LastZScore", finalZScore);
            PlayerPrefs.Save();
            scoreSaved = true;
        }
    }
}
