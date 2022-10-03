// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PartyTimer : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource chatterPlayer;

    float chatterVolume;
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
        chatterPlayer.PlayScheduled(startTime);
        chatterVolume = chatterPlayer.volume;
        Party.onGameOver += OnGameOver;
    }

    void OnDisable()
    {
        musicPlayer.Stop();
        chatterPlayer.Stop();
        Party.onGameOver -= OnGameOver;
    }

    void Update()
    {
        if (dspTime >= lightsOffEvent)
        {
            Party.onLightsOff.Invoke();
            lightsOffEvent += loopLength;
            chatterPlayer.volume = 0;
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
            chatterPlayer.volume = chatterVolume;
        }

        if (dspTime >= endTime )
        {
            Party.onGameOver.Invoke();
            enabled = false;
        }
    }

    void OnGameOver()
    {
        enabled = false;
    }
}
