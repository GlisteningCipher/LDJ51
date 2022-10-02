// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PartyTimer : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;

    double dspTime => AudioSettings.dspTime;

    double startTime;
    double lightsOffEvent;
    double lightsOnEvent;
    double resumePartyEvent;

    static double loopLength = 12;

    void OnEnable()
    {
        startTime = AudioSettings.dspTime + 1;
        lightsOffEvent = startTime + 10;
        lightsOnEvent = startTime + 11;
        resumePartyEvent = startTime;
        musicPlayer.PlayScheduled(startTime);
    }

    void Update()
    {
        if (dspTime >= lightsOffEvent)
        {
            Debug.Log("Lights Off");
            lightsOffEvent += loopLength;
        }
        
        if (dspTime >= lightsOnEvent)
        {
            Debug.Log("Lights On");
            lightsOnEvent += loopLength;
        }
        
        if (dspTime >= resumePartyEvent)
        {
            Debug.Log("Resume Party");
            resumePartyEvent += loopLength;
        }
    }
}
