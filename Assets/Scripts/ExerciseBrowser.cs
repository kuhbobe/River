using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ExerciseBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;


    private void OnEnable()
  {
    for(int i = 0; i < GameManager.Instance.levelCounts[GameManager.Instance.currentWorld]; i++){
        GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
        int levelNum = i + 1;
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = levelNum.ToString(); // Just the number
            buttonText.fontSize = 64; // Bigger font size
            buttonText.fontStyle = FontStyles.Bold; // Make it bold
            buttonText.alignment = TextAlignmentOptions.Center; // Center text
        }
        newButton.GetComponent<Button>().onClick.AddListener(() => SelectLevel(levelNum));
        Debug.Log("loading" + i);
    }
  }

  private void SelectLevel(int level)
  {
      SceneManager.LoadScene("Exercise " + level);
  }
}
