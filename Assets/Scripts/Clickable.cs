using UnityEngine;
using System;

public class Clickable : MonoBehaviour
{
    public event Action onClick;

    void OnMouseDown()
    {
        onClick.Invoke();
    }
}
