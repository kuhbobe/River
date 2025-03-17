using UnityEngine;
using UnityEngine.UI;

public class ExerciseBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;


    private void OnEnable()
  {
    for(int i = 0; i < GameManager.Instance.levelCounts[GameManager.Instance.currentWorld]; i++){
        GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
        int levelNum = i + 1;
        newButton.GetComponent<Button>().onClick.AddListener(() => SelectLevel(levelNum));
        Debug.Log("loading" + i);
    }
  }

  private void SelectLevel(int level)
  {
      Debug.Log("Loading Exercise" + level);
  }
}
