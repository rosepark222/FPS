using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class ObjToSpawnScript : NetworkBehaviour
{
    Rigidbody rb;
    CharacterController characterController;
    Vector3 moveDirection; // = Vector3.zero;
    public float bulletSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start in ObjToSpawnScript");
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //moveDirection = (forward * bulletSpeed);
        //transform.position += moveDirection;
        //characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        moveDirection = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update in ObjToSpawnScript");

        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //moveDirection += (forward * bulletSpeed);
        //transform.position += moveDirection;
        //characterController.Move(moveDirection * Time.deltaTime);
        //rb.Move(moveDirection * Time.deltaTime, Quaternion.identity);
        //rb.Move(transform.position, Quaternion.identity);


        moveDirection += Vector3.forward * bulletSpeed * Time.deltaTime;
        rb.Move(moveDirection, Quaternion.identity);


    }
}
