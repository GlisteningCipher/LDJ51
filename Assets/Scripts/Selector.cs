// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] LayerMask uiLayer;
    [SerializeField] ArrestUI arrestUI;

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

            if (selection) arrestUI.Open(); else arrestUI.Close();
        }
    }

    public void ArrestSelection()
    {
        arrestUI.Close();
        if (!selection) return;
        Destroy(selection.transform.parent.gameObject); //remove guest from party
        enabled = false;
        Party.onLightsOn += OnLightsOn;
    }

    void OnLightsOn()
    {
        enabled = true;
        Party.onLightsOn -= OnLightsOn;
    }
}
