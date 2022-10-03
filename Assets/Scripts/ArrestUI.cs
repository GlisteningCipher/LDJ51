using UnityEngine;
using DG.Tweening;

public class ArrestUI : MonoBehaviour
{
    RectTransform rect => (RectTransform)transform;

    [ContextMenu("Open")]
    void Open()
    {
        rect.DOAnchorPosX(0,1f);
    }

    [ContextMenu("Close")]
    void Close()
    {
        rect.DOAnchorPosX(250,1f);
    }
}
