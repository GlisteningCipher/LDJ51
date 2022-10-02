using UnityEngine;

public class Murderer : MonoBehaviour
{
    [SerializeField] Clickable myClickable;
    [SerializeField] Collider2D knifeRange;
    [SerializeField] ContactFilter2D filter;

    void Awake()
    {
        myClickable.onClick += ClickResponse;
        Party.onLightsOff += Kill;
    }

    void OnDestroy()
    {
        myClickable.onClick -= ClickResponse;
        Party.onLightsOff -= Kill;
    }

    void ClickResponse()
    {
        Debug.Log("Drat! You found me!");
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
