using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ArrestUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Range(0f,1f)] float duration = 0.5f;
    [SerializeField] CanvasGroup group;

    [SerializeField] AudioSource open;

    RectTransform rect => (RectTransform)transform;

    bool isOpen;

    [ContextMenu("Open")]
    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        rect.DOAnchorPosX(0, duration);
        open.Play();
    }

    [ContextMenu("Close")]
    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;
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
