using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ArrestUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Range(0f,1f)] float duration = 0.5f;
    [SerializeField] CanvasGroup group;
    RectTransform rect => (RectTransform)transform;

    [ContextMenu("Open")]
    public void Open()
    {
        rect.DOAnchorPosX(0, duration);
    }

    [ContextMenu("Close")]
    public void Close()
    {
        rect.DOAnchorPosX(250, duration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        group.DOFade(1f, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        group.DOFade(0.33f, duration);
    }
}
