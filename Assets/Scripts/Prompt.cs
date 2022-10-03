using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class Prompt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CanvasGroup card;
    [SerializeField] UnityEvent onComplete;

    Tween fade;

    void Start()
    {
        text.alpha = 0.2f;
        fade = text.DOFade(1, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            fade.Kill();
            text.enabled = false;
            card.DOFade(0, 1f).OnComplete(() => {
                onComplete.Invoke();
            });
        }
    }
}
