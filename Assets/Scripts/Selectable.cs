// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material standardMaterial;
    [SerializeField] SpriteRenderer[] renderers;

    [ContextMenu("Select")]
    public void Select()
    {
        foreach (var renderer in renderers) renderer.material = highlightMaterial;
    }

    [ContextMenu("Deselect")]
    public void Deselect()
    {
        foreach (var renderer in renderers) renderer.material = standardMaterial;
    }

    void OnDestroy()
    {
        Deselect();
    }
}
