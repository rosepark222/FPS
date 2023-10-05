using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerSpawnObject : NetworkBehaviour
{
    public GameObject objToSpawn;
    [HideInInspector] public GameObject spwanedObject;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            GetComponent<PlayerSpawnObject>().enabled = false;
    }

    private void Update()
    {
        if(spwanedObject == null && Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (objToSpawn == null)
            {
                Debug.Log("objToSpawn is null");
            }else
            {
                Debug.Log("objToSpawn is not null");
            }

            SpawnObject(objToSpawn, transform, this);
        }
        if (spwanedObject != null && Input.GetKeyDown(KeyCode.Alpha2))
        {
            DespawnObject(spwanedObject);
        }

    }

    [ServerRpc]
    public void SpawnObject(GameObject obj, Transform player, PlayerSpawnObject script)
    {
        GameObject spawned = Instantiate(obj, player.position + player.forward, Quaternion.identity);
        ServerManager.Spawn(spawned);
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
