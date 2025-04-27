using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyboxManager : MonoBehaviour
{
    public static SkyboxManager Instance;

    [Header("Skybox Options")]
    public Material[] skyboxes;
    private int currentIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            currentIndex = PlayerPrefs.GetInt("SkyboxIndex", 0);
            ApplySkybox(currentIndex);

            // Listen for new scenes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-apply the current skybox
        ApplySkybox(currentIndex);
    }

    public void SetSkyboxByIndex(int index)
    {
        if (index >= 0 && index < skyboxes.Length)
        {
            currentIndex = index;
            ApplySkybox(index);
            PlayerPrefs.SetInt("SkyboxIndex", index);
            PlayerPrefs.Save();
        }
    }

    private void ApplySkybox(int index)
    {
        RenderSettings.skybox = skyboxes[index];
        DynamicGI.UpdateEnvironment();
    }

    public int GetCurrentSkyboxIndex() => currentIndex;
}
