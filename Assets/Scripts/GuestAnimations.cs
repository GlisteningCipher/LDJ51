using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestAnimations : MonoBehaviour
{
    private Animator animator;
    private int animationType;
    void Start()
    {
        animator = GetComponent<Animator>();
        animationType = Random.Range(1,5);
        StartCoroutine(WaitTillIdle(Random.Range(0, 1)));
    }

    IEnumerator WaitTillIdle(float wait)
    {
        yield return new WaitForSeconds(wait);
        animator.SetInteger("IdleState", animationType);
    }

}
