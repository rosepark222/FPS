using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class ObjToSpawnScript : NetworkBehaviour
{
    Rigidbody rb;
    CharacterController characterController;
    Vector3 nextPosition; // = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]   Vector3 forward;
    [SerializeField] float bulletSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start in ObjToSpawnScript");
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //nextPosition = (forward * bulletSpeed);
        //transform.position += nextPosition;
        //characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        nextPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        forward = transform.TransformDirection(Vector3.forward);
        //Debug.Log(string.Format("Update in ObjToSpawnScript {0} {1} {2}", forward.x, forward.y, forward.z));

        //nextPosition += (forward * bulletSpeed);
        //transform.position += nextPosition;
        //characterController.Move(nextPosition * Time.deltaTime);
        //rb.Move(nextPosition * Time.deltaTime, Quaternion.identity);
        //rb.Move(transform.position, Quaternion.identity);


        nextPosition += Vector3.forward * bulletSpeed * Time.deltaTime;
        //rb.Move(nextPosition, Quaternion.identity);

    }
}
