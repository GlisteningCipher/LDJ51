using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] GameObject guestPrefab;
    [SerializeField] int guestsAtStart = 50;

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
        for (int i = 0; i < guestsAtStart; i++)
        {
            var spawnPos = GetRandomPoint();
            Instantiate(guestPrefab, spawnPos, Quaternion.identity, transform);
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
}
