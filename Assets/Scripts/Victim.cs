using UnityEngine;

public class Victim : MonoBehaviour
{
    public void GetStabbed()
    {
        Die();
    }

    public void GetShot()
    {
        Die();
    }

    void Die()
    {
        transform.parent.localScale = new Vector3(1f, 0.25f, 1f);
        Destroy(GetComponentInParent<Wander>());
        Destroy(gameObject);
    }
}
