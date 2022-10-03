using UnityEngine;

public class MissTracker : MonoBehaviour
{
    int misses = 0;
    int maxMisses = 3;
    [SerializeField] AudioSource wrongPerson;

    void Awake()
    {
        Party.onGameStart += HandleGameStart;
    }

    void OnDestroy()
    {
        Party.onGameStart -= HandleGameStart;
    }

    public void AddMark()
    {
        transform.GetChild(misses).gameObject.SetActive(true);
        misses += 1;

        if (misses == maxMisses) { Party.onLightsOn += HandleGameOver; }
        else Party.onLightsOn += HandleWrongGuess;
    }

    void HandleWrongGuess()
    {
        wrongPerson.Play();
        Party.onLightsOn -= HandleWrongGuess;
    }

    void HandleGameOver()
    {
        Party.deaths = 15;
        Party.onGameOver.Invoke();
        Party.onLightsOn -= HandleGameOver;
    }

    void HandleGameStart()
    {
        misses = 0;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
