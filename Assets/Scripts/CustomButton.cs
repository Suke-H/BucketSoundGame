using UnityEngine;  
using UnityEngine.EventSystems;  

public class CustomButton : MonoBehaviour,  
    IPointerClickHandler
    // IPointerDownHandler,  
    // IPointerUpHandler  
{
    public System.Action onClickCallback;  

    // [SerializeField] private CanvasGroup _canvasGroup;

    public void OnPointerClick(PointerEventData eventData)  
    {
        onClickCallback?.Invoke();
    }
}
