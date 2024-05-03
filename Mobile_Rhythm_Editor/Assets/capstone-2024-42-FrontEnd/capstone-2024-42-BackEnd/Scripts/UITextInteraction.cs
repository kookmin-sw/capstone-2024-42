using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;


public class UITextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [System.Serializable]
    private class OnclickEvent : UnityEvent { }

    [SerializeField]
    private OnclickEvent onClickEvent;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Normal;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
    }
}
