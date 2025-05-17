using UnityEngine;

public class BallTouchDestroy : MonoBehaviour
{
    public string destroyTag = "Glove";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(destroyTag))
        {
            BallSpawner.score++; 
            Debug.Log("Score: " + BallSpawner.score);

            Destroy(gameObject);
            AudioManager.Instance?.PlaySFX("Click");
        }
    }
}
