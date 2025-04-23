using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudioFeedback : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
  

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Hover");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Click");
        }
    }

    
}
