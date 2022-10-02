using UnityEngine;

public class Murderer : MonoBehaviour
{
    [SerializeField] Collider2D knifeRange;
    [SerializeField] ContactFilter2D filter;

    void Awake()
    {
        Party.onLightsOff += Kill;
    }

    void OnDestroy()
    {
        Party.onLightsOff -= Kill;
    }

    [ContextMenu("Kill")]
    void Kill()
    {
        if (TargetInKnifeRange(out var victim)) victim.GetStabbed();
        else ShootRandomTarget();
    }

    bool TargetInKnifeRange(out Victim victim)
    {
        victim = null;
        var results = new Collider2D[5];
        var numResults = knifeRange.OverlapCollider(filter, results);
        if (numResults > 0)
        {
            var col = results[Random.Range(0, numResults)];
            victim = col.GetComponent<Victim>();
            return true;
        }
        return false;
    }

    void ShootRandomTarget()
    {
        var victim = GetRandomVictim();
        victim.GetShot();
    }

    Victim GetRandomVictim()
    {
        Victim[] targets = transform.parent.GetComponentsInChildren<Victim>();
        return targets[Random.Range(0, targets.Length)];
    }
}
