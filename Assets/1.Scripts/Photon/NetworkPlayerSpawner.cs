using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("XR Interaction Hands Setup Variant", transform.position, transform.rotation);
        //spawnedPlayerPrefab = PhotonNetwork.Instantiate("XR Interaction Hands Setup Variant", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
