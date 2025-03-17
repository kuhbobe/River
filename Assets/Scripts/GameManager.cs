using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int[] levelCounts;
    public int currentWorld;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
