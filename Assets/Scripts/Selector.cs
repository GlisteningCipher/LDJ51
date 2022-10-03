// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] LayerMask uiLayer;

    public Selectable selection;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (selection)
            {
                selection.Deselect();
                selection = null;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, uiLayer);

            if (hit.collider != null)
            {
                selection = hit.collider.GetComponent<Selectable>();
                selection.Select();
            }
        }
    }
}
