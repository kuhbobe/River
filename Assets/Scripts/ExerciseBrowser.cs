using UnityEngine;
using UnityEngine.UI;

public class ExerciseBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;


    private void OnEnable()
  {
    for(int i = 0; i < 0; i++){
        GameObject newExercise = Instantiate(buttonPrefab, buttonParent.transform);
    }
  }
}
