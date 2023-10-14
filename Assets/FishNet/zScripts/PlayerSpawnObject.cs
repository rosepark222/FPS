using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerSpawnObject : NetworkBehaviour
{
    public GameObject objToSpawn;
    [HideInInspector] public GameObject spwanedObject;
    public float bulletSpeed = 10f;
    [SerializeField] Camera playerCamera;
    [SerializeField] Vector3 shootDirection;
    [SerializeField] Vector3 cameraForward;
//    public ShootingInfo shootinfo;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            GetComponent<PlayerSpawnObject>().enabled = false;

        

    }

    private void Update()
    {
        if(spwanedObject == null && Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Alpha1)) //Input.GetMouseButtonDown(0))
        {

            cameraForward = Camera.main.transform.forward.normalized;
            shootDirection = cameraForward;
            Debug.Log(string.Format("1.7 shoot direction {0} {1} {2}", shootDirection.x, shootDirection.y, shootDirection.z));
//            shootinfo.direction = shootDirection;
//            shootinfo.UpdateScore();
            SpawnObject(objToSpawn, transform, this, shootDirection);
        }
        if (spwanedObject != null && Input.GetMouseButtonDown(1)) // Input.GetKeyDown(KeyCode.Alpha2)) //Input.GetMouseButtonDown(1))
        {
            DespawnObject(spwanedObject);
        }

    }

    [ServerRpc]
    public void SpawnObject(GameObject obj, Transform player, PlayerSpawnObject script, Vector3 shootDirection)
    {
        Vector3 ballPosition = player.position + player.forward;

        GameObject spawned = Instantiate(obj, ballPosition, Quaternion.identity);
        //spawned.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * bulletSpeed;

        Debug.Log(string.Format("1 serverRPC: location for the ball is {0} {1} {2}", ballPosition.x, ballPosition.y, ballPosition.z));

        ObjToSpawnScript objscript = spawned.GetComponent<ObjToSpawnScript>();
        objscript.moveDirection = shootDirection;
        objscript.nextPosition = ballPosition; // playerCamera.transform.forward;
        Debug.Log(string.Format("1.1 serverRPC: objscript moveDirection for the ball is {0} {1} {2}", objscript.moveDirection.x, objscript.moveDirection.y, objscript.moveDirection.z));

 

        Debug.Log(string.Format("4 serverRPC: initial move dir for the ball is {0} {1} {2}", shootDirection.x, shootDirection.y, shootDirection.z));


        ServerManager.Spawn(spawned, base.Owner);
        SetSpawnedObject(spawned, script);
    }

    [ObserversRpc]
    public void SetSpawnedObject(GameObject spawned, PlayerSpawnObject script)
    {
        script.spwanedObject = spawned;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnObject(GameObject obj)
    {
        ServerManager.Despawn(obj);
    }

}
