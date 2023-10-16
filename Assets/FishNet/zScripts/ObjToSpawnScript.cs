using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class ObjToSpawnScript : NetworkBehaviour
{
    [SerializeField] GameObject objToSpawn;
    Rigidbody rb;
    CharacterController characterController;
    [SyncVar] public Vector3 nextPosition; // = Vector3.zero; // new Vector3(434.0f, 218.5f, -3.4f);
    [SerializeField]  public Vector3 initialPosition;
    [SerializeField]  public Vector3 moveDirection; // = Vector3.zero;
    [SerializeField]   Vector3 forward;
    [SerializeField] float bulletSpeed = 60f;
    public static event System.Action<int> ChangeScoreEvent;


    private Camera playerCameraForward;


    private void Awake()
    {
        nextPosition = initialPosition;
        Debug.Log(string.Format("Awake in ObjToSpawnScript nextPosition = initialPosition; {0} {1} {2}", nextPosition.x, nextPosition.y, nextPosition.z));


    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start in ObjToSpawnScript before checking ownership");
        if (!base.IsOwner) return;
        Debug.Log("Start in ObjToSpawnScript after checking ownership");
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //nextPosition = (forward * bulletSpeed);
        //transform.position += nextPosition;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        nextPosition = transform.position;
        Debug.Log(string.Format("Start in ObjToSpawnScript nextPosition {0} {1} {2}", nextPosition.x, nextPosition.y, nextPosition.z));

        // in server, this velicity makes the ball to travel in line
        // in different client, the position of the ball was not in sync -- makes the ball to drop and move slowly
        // rb.velocity = Camera.main.transform.forward * bulletSpeed;
        //moveDirection = Camera.main.transform.forward;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

       // Debug.Log(string.Format("Update in ObjToSpawnScript  "));

        if (base.IsServer)
        {
            //forward = transform.TransformDirection(Vector3.forward);

            //nextPosition += (forward * bulletSpeed);
            //transform.position += nextPosition;
            //characterController.Move(nextPosition * Time.deltaTime);
            //rb.Move(nextPosition * Time.deltaTime, Quaternion.identity);
            //rb.Move(transform.position, Quaternion.identity);
            //moveDirection = forward;

            nextPosition = nextPosition + moveDirection * bulletSpeed * Time.deltaTime;
            Debug.Log(string.Format("Update in ObjToSpawnScript Server nextPosition {0} {1} {2}", nextPosition.x, nextPosition.y, nextPosition.z));

        }

        //if (!base.IsOwner) return;
        if (base.IsClient)
        {
            float lerpSpeed = 0.5f; // a large lerpSpeed makes the movement choppy

            Vector3 nextPosition2 = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * lerpSpeed);

            Debug.Log(string.Format("Update in ObjToSpawnScript client nextPosition {0} {1} {2}", nextPosition.x, nextPosition.y, nextPosition.z));

            // this forceful update of the position is synced between the server and client
            // other player can see the ball move in a line
            Rigidbody rb = objToSpawn.GetComponent<Rigidbody>();

            // transform.position = nextPosition;
            // transform.position = nextPosition2;

//            rb.Move(nextPosition, Quaternion.identity);
            rb.Move(nextPosition2, Quaternion.identity);

            //characterController.Move(nextPosition * Time.deltaTime);
            //MoveObject2(transform, nextPosition);
            // MoveObject(objToSpawn, nextPosition);
        }

    }

    [ServerRpc]
    public void MoveObject(GameObject obj, Vector3 nextPosition) // , Transform player, PlayerSpawnObject script)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.Move(nextPosition, Quaternion.identity);
        //GameObject spawned = Instantiate(obj, player.position + player.forward, Quaternion.identity);
        //spawned.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * bulletSpeed;

        //ServerManager.Spawn(spawned);
        //SetSpawnedObject(spawned, script);
    }

    [ServerRpc]
    public void MoveObject2(Transform t, Vector3 nextPosition) // , Transform player, PlayerSpawnObject script)
    {
        t.position = nextPosition;
        //Rigidbody rb = obj.GetComponent<Rigidbody>();
        //rb.Move(nextPosition, Quaternion.identity);
        //GameObject spawned = Instantiate(obj, player.position + player.forward, Quaternion.identity);
        //spawned.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * bulletSpeed;

        //ServerManager.Spawn(spawned);
        //SetSpawnedObject(spawned, script);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);

        // CS0070: The event 'PlayerSpawnObject.ChangedLengthEvent' can only appear on the left hand side of += or -= (except when used from within the type 'PlayerSpawnObject')
        // PlayerSpawnObject.ChangedLengthEvent?.Invoke(transform.position);

        Debug.Log(string.Format("ball collides to {0}, {1}", other.gameObject.name, other.gameObject.tag));
        if (other.gameObject.CompareTag("structure"))
        {

            ChangeScoreEvent?.Invoke(1);
            DespawnObject(objToSpawn);

        }
        else if (other.gameObject.CompareTag("Player")) // enemy hit
        {
            Debug.Log(string.Format("ball collides to enemy {0}, {1}", other.gameObject.name, other.gameObject.tag));
            ChangeScoreEvent?.Invoke(10);
            DespawnObject(objToSpawn);
        }
    }

    [ServerRpc(RequireOwnership = true)]
    public void DespawnObject(GameObject obj)
    {
        ServerManager.Despawn(obj);
    }
}
