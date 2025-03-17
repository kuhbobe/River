using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject ExerciseBrowser;
    public void ShowLevels(int world)
    {
        GameManager.Instance.currentWorld = world;

    }
    
    
}
