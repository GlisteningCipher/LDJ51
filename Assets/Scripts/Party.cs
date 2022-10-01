using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Party : MonoBehaviour
{
    [SerializeField] GameObject victimPrefab;
    [SerializeField] int startingVictims = 49;
    [SerializeField] GameObject murdererPrefab;
    [SerializeField] int startingMurderers = 1;

    public static event Action onLightsOff;
    public static event Action onLightsOn;

    static float ROOM_HALFWIDTH = 12.8f;
    static float ROOM_HALFHEIGHT = 8f;

    void OnEnable()
    {
        StartParty();
    }

    void OnDisable()
    {
        EndParty();
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
        
        TurnLightsOn();
    }

    void EndParty()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    [ContextMenu("Lights Off")]
    void TurnLightsOff()
    {
        onLightsOff.Invoke();
    }

    [ContextMenu("Lights On")]
    void TurnLightsOn()
    {
        onLightsOn.Invoke();
    }

    public static Vector2 GetRandomPoint()
    {
        return new Vector2(
            Random.Range(-Party.ROOM_HALFWIDTH, Party.ROOM_HALFWIDTH),
            Random.Range(-Party.ROOM_HALFHEIGHT, Party.ROOM_HALFHEIGHT));
    }
}
