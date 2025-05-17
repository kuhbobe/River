using UnityEngine;
using TMPro;

public class SkyboxDropdownHandler : MonoBehaviour
{
    public TMP_Dropdown skyboxDropdown;

    void Start()
    {
       
        int currentIndex = SkyboxManager.Instance.GetCurrentSkyboxIndex();
        skyboxDropdown.value = currentIndex;
        skyboxDropdown.RefreshShownValue();

     
        skyboxDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void OnDropdownChanged(int index)
    {
        SkyboxManager.Instance.SetSkyboxByIndex(index);
    }
}
