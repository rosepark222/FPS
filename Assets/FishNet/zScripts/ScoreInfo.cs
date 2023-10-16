using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FishNet.Object;
using TMPro;
using Unity.VisualScripting;
using System;

public class ScoreInfo : MonoBehaviour
{
 
    // https://youtu.be/swIM2z6Foxk?si=kLkuSVKODDR8L0mU&t=5569
    // The Ultimate Multiplayer Tutorial for Unity - Netcode for GameObjects
    //  by samyam
    [SerializeField] private TextMeshProUGUI ScoreText;
    int score = 0;


    private void OnEnable()
    {
        ObjToSpawnScript.ChangeScoreEvent += ChangeScoreText;

    }

    private void OnDisable()
    {
        throw new NotImplementedException(); //  PlayerSpawnObject.ChangedLengthEvent += ChangeLengthText;
    }

    private void ChangeScoreText(int score)
    {
        this.score += score;
        ScoreText.text = string.Format("Score: {0}", this.score);
    }
    /*
    private void Start()
    {
        direction.x = 3f;
        direction.y = 4f;
        direction.z = 5f;
        ShootDirection.text = string.Format("beginning with {0} {1} {2}", direction.x, direction.y, direction.z);
        ShootDirection.text = string.Format("beginning2 with {0} {1} {2}", direction.x, direction.y, direction.z);
        ShootDirection.text = string.Format("beginning3 with {0} {1} {2}", direction.x, direction.y, direction.z);


    }

    public void UpdateScore()
    {
        Debug.Log(string.Format("1.8 shoot direction {0} {1} {2}", direction.x, direction.y, direction.z));

        ShootDirection.text = string.Format(" shoot {0}, {1}, {2}", direction.x, direction.y, direction.z);
    }

    public void Update()
    {
       // ShootDirection.text = string.Format(" in Update shoot {0}, {1}, {2}", count, direction.y, direction.z);
       // count += 1;

    }
    */
}
