using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FishNet.Object;
using TMPro;
using Unity.VisualScripting;
using System;

public class ShootingInfo : MonoBehaviour
{
    [SerializeField] public Text ShootDirection;
    public Vector3 direction; // = new Vector3();
    float count = 0;

    // https://youtu.be/swIM2z6Foxk?si=kLkuSVKODDR8L0mU&t=5569
    // The Ultimate Multiplayer Tutorial for Unity - Netcode for GameObjects
    //  by samyam
    [SerializeField] private TextMeshProUGUI lengthText;


    private void OnEnable()
    {
        PlayerSpawnObject.ChangedLengthEvent += ChangeLengthText;
    }

    private void OnDisable()
    {
        throw new NotImplementedException(); //  PlayerSpawnObject.ChangedLengthEvent += ChangeLengthText;
    }

    private void ChangeLengthText(Vector3 direction)
    {
        lengthText.text = string.Format("shoot event with {0} {1} {2}", direction.x, direction.y, direction.z);
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
