using UnityEngine;
using UnityEngine.UI;

public class Blindfold : MonoBehaviour
{
    [SerializeField] Image image;

    void Awake()
    {
        image.enabled = true;
        gameObject.SetActive(false);
        Party.onLightsOff += Activate;
        Party.onLightsOn += Deactivate;
    }

    void OnDestroy()
    {
        Party.onLightsOff -= Activate;
        Party.onLightsOn -= Deactivate;
    }

    void Activate()
    {
        gameObject.SetActive(true);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
