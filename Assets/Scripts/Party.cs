using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class Party : MonoBehaviour
{
    [SerializeField] GameObject victimPrefab;
    [SerializeField] int startingVictims = 49;
    [SerializeField] GameObject murdererPrefab;
    [SerializeField] int startingMurderers = 1;

    public static Action onLightsOff;
    public static Action onLightsOn;
    public static Action onResumeParty;

    public static Action onGameOver;

    static float ROOM_HALFWIDTH = 12.8f;
    static float ROOM_HALFHEIGHT = 8f;

    void OnEnable()
    {
        StartParty();
        onResumeParty += CheckGameState;
    }

    void OnDisable()
    {
        EndParty();
        onResumeParty -= CheckGameState;
    }

    void StartParty()
    {
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
    
    public void CheckGameState()
    {
        var murderer = GameObject.FindGameObjectWithTag("Murderer");
        if (!murderer) onGameOver.Invoke();
    }
    
    public void Reset()
    {
        EndParty(); StartParty();
    }
}
