using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Party : MonoBehaviour
{
    [SerializeField] GameObject victimPrefab;
    [SerializeField] GameObject murdererPrefab;
    [SerializeField] int startingVictims = 49;
    [SerializeField] int startingMurderers = 1;

    public static Action onLightsOff;
    public static Action onLightsOn;
    public static Action onResumeParty;

    public static Action onGameOver;
    public static Action onGameStart;

    public static int deaths;

    static float ROOM_HALFWIDTH = 12.8f;
    static float ROOM_HALFHEIGHT = 8f;

    void OnEnable()
    {
        StartParty();
        onResumeParty += CheckForMurderers;
    }

    void OnDisable()
    {
        EndParty();
        onResumeParty -= CheckForMurderers;
    }

    void StartParty()
    {
        Party.onLightsOn.Invoke();
        for (int i = 0; i < startingVictims; i++)
        {
            var spawnPos = GetRandomPoint();
            Instantiate(victimPrefab, spawnPos, Quaternion.identity, transform);
        }

        for (int i = 0; i < startingMurderers; i++)
        {
            var spawnPos = GetRandomPoint();
            Instantiate(murdererPrefab, spawnPos, Quaternion.identity, transform);
        }

        var behaviours = GetComponentsInChildren<Behaviour>();
        foreach(var behaviour in behaviours) behaviour.enabled = true;

        deaths = 0;
        onGameStart.Invoke();
    }

    void EndParty()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public static Vector2 GetRandomPoint()
    {
        return new Vector2(
            Random.Range(-Party.ROOM_HALFWIDTH, Party.ROOM_HALFWIDTH),
            Random.Range(-Party.ROOM_HALFHEIGHT, Party.ROOM_HALFHEIGHT));
    }
    
    public void CheckForMurderers()
    {
        var murderer = GameObject.FindGameObjectWithTag("Murderer");
        if (!murderer) onGameOver.Invoke();
    }

    public void Reset()
    {
        EndParty(); StartParty();
    }
}
