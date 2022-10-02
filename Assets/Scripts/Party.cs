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

    public static event Action onLightsOff;
    public static event Action onLightsOn;
    public static event Action onResumeParty;

    static float ROOM_HALFWIDTH = 12.8f;
    static float ROOM_HALFHEIGHT = 8f;

    // bool lightsOn = true;

    Coroutine mainLoop;

    void OnEnable()
    {
        StartParty();
    }

    void OnDisable()
    {
        EndParty();
    }

    void Update()
    {
        // if (Input.GetButtonUp("Fire1"))
        // {
        //     lightsOn = !lightsOn;
        //     if (lightsOn) TurnLightsOn(); else TurnLightsOff();
        // }
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

        onResumeParty.Invoke();
        mainLoop = StartCoroutine(MainLoop());
    }

    void EndParty()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        StopCoroutine(mainLoop);
    }

    // [ContextMenu("Lights Off")]
    // void TurnLightsOff()
    // {
    //     onLightsOff.Invoke();
    // }

    // [ContextMenu("Lights On")]
    // void TurnLightsOn()
    // {
    //     onLightsOn.Invoke();
    //     StartCoroutine(ResumeParty(1f));
    // }

    IEnumerator TurnLightsOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        onLightsOff.Invoke();
    }
    
    IEnumerator TurnLightsOn(float delay)
    {
        yield return new WaitForSeconds(delay);
        onLightsOn.Invoke();
    }

    IEnumerator ResumeParty(float delay)
    {
        yield return new WaitForSeconds(delay);
        onResumeParty.Invoke();
    }

    IEnumerator MainLoop()
    {
        while (true)
        {
            yield return TurnLightsOff(7f);
            yield return TurnLightsOn(0.5f);
            yield return ResumeParty(2.5f);
        }
    }

    public static Vector2 GetRandomPoint()
    {
        return new Vector2(
            Random.Range(-Party.ROOM_HALFWIDTH, Party.ROOM_HALFWIDTH),
            Random.Range(-Party.ROOM_HALFHEIGHT, Party.ROOM_HALFHEIGHT));
    }
}
