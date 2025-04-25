using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ExerciseBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    // Add your custom level names here. Index 0 = Level 1, Index 1 = Level 2, etc.
    public string[] customLevelNames = {
        "Mobility",
        "Slicing",
        "Stretch"
    };

    private void OnEnable()
    {
        int totalLevels = GameManager.Instance.levelCounts[GameManager.Instance.currentWorld];

        for (int i = 0; i < totalLevels; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
            int levelNum = i + 1;

            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                string levelName = (i < customLevelNames.Length) ? customLevelNames[i] : "Exercise " + levelNum;
                buttonText.text = $"{levelNum}. {levelName}";
                buttonText.fontSize = 64;
                buttonText.fontStyle = FontStyles.Bold;
                buttonText.alignment = TextAlignmentOptions.Center;
            }

            newButton.GetComponent<Button>().onClick.AddListener(() => SelectLevel(levelNum));
            Debug.Log("loading " + levelNum);
        }
    }

    private void SelectLevel(int level)
    {
        SceneManager.LoadScene("Exercise " + level);
    }
}
