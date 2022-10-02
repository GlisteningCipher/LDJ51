// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PartyTimer : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;

    double dspTime => AudioSettings.dspTime;

    static double startTime;
    static double lightsOffEvent;
    static double lightsOnEvent;
    static double resumePartyEvent;
    static double lastLoop => startTime + 168 + 1; //added 1 beat to trigger resume on last loop
    static double endTime => startTime + runtime;

    const double loopLength = 12;
    const double runtime = 300;

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
            Party.onLightsOff.Invoke();
            lightsOffEvent += loopLength;
        }
        
        if (dspTime >= lightsOnEvent && dspTime <= lastLoop)
        {
            Party.onLightsOn.Invoke();
            lightsOnEvent += loopLength;
        }
        
        if (dspTime >= resumePartyEvent && dspTime <= lastLoop)
        {
            Party.onResumeParty.Invoke();
            resumePartyEvent += loopLength;
        }

        if (dspTime >= endTime )
        {
            Debug.Log("The End!");
            enabled = false;
        }
    }
}
