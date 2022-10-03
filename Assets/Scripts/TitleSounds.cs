using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSounds : MonoBehaviour
{
    [SerializeField] AudioSource open;
    [SerializeField] AudioSource close;

    public void Prompt()
    {
        open.Play();
    }
    public void Invite()
    {
        StartCoroutine(Invite(1));
    }
    IEnumerator Invite(float wait)
    {
        close.Play();
        yield return new WaitForSeconds(wait);
        open.Play();
    }
    public void Intel()
    {
        close.Play();
    }



}
