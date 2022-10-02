using UnityEngine;
using UnityEngine.UI;

public class Blindfold : MonoBehaviour
{
    [SerializeField] Image image;

    void Awake()
    {
        image.enabled = false;
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
        image.enabled = true;
    }

    void Deactivate()
    {
        image.enabled = false;
    }
}
