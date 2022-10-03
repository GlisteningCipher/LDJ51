using UnityEngine;

public class MissTracker : MonoBehaviour
{
    int misses = 0;
    int maxMisses = 3;

    public void AddMark()
    {
        transform.GetChild(misses).gameObject.SetActive(true);
        misses += 1;

        if (misses == maxMisses) Party.onLightsOn += HandleGameOver;
    }

    void HandleGameOver()
    {
        Party.onGameOver.Invoke();
    }
}
