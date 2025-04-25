using UnityEngine;
using TMPro;

public class SkyboxDropdownHandler : MonoBehaviour
{
    public TMP_Dropdown skyboxDropdown;

    void Start()
    {
        // Optional: Set dropdown value to current skybox index
        int currentIndex = SkyboxManager.Instance.GetCurrentSkyboxIndex();
        skyboxDropdown.value = currentIndex;
        skyboxDropdown.RefreshShownValue();

        // Add listener
        skyboxDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void OnDropdownChanged(int index)
    {
        SkyboxManager.Instance.SetSkyboxByIndex(index);
    }
}
