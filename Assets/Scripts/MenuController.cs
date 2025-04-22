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

    public void MainScreen () {
        SceneManager.LoadScene("Start");
    }
    
    public void ExitApp()
    {
   
        Application.Quit();

    }
    
}
