using System;
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
        GetComponentInParent<Rigidbody2D>().mass = 9999;
        Destroy(GetComponentInParent<Wander>());
        Destroy(gameObject);
    }
}
