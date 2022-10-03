// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Letter : MonoBehaviour
{
    [SerializeField] UnityEvent onComplete;

    RectTransform rect => (RectTransform)transform;

    void OnEnable()
    {
        rect.DOAnchorPosY(0,0.5f);
    }

    void OnDisable()
    {
        rect.DOAnchorPosY(-500, 1f).OnComplete(() => { onComplete.Invoke(); });
    }
}
