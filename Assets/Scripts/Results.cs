// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Results : MonoBehaviour
{
    [SerializeField, Multiline] string[] letters;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] TextMeshProUGUI casualties;
    [SerializeField] Transform starContainer;
    [SerializeField] AudioSource cheering;
    [SerializeField] AudioSource youLose;
    int stars;

    void Awake()
    {
        Party.onGameOver += HandleGameOver;
    }

    void OnDestroy()
    {
        Party.onGameOver -= HandleGameOver;
    }

    void OnEnable()
    {
        switch (Party.deaths)
        {
            case 0:
                message.text = letters[0];
                stars = 5;
                break;
            case 1: case 2:
                message.text = letters[1];
                stars = 5;
                break;
            case 3: case 4: case 5:
                message.text = letters[2];
                stars = 4;
                break;
            case 6: case 7: case 8:
                message.text = letters[3];
                stars = 3;
                break;
            case 9: case 10: case 11:
                message.text = letters[4];
                stars = 2;
                break;
            case 12: case 13: case 14:
                message.text = letters[5];
                stars = 1;
                break;
            case 15:
                message.text = letters[6];
                stars = 0;
                break;
        }
        if (Party.deaths != 15) cheering.Play();
        else youLose.Play();
        casualties.text = Party.deaths <15 ? $"Loss of Life: {Party.deaths}" : "Loss of Life: Everyone";

        foreach (Transform star in starContainer)
            star.gameObject.SetActive(star.GetSiblingIndex() < stars);
        

        ((RectTransform)transform).DOAnchorPosY(0, 0.5f);
        
    }

    void OnDisable()
    {
       
        ((RectTransform)transform).DOAnchorPosY(-500, 0.5f);
        Party.deaths = 0;
    }

    void HandleGameOver()
    {
        enabled = true;
    }
}
