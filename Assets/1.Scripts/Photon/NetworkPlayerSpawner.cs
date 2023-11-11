using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

    void Start()
    {
        //if (photonView.IsMine)
        //{
        //    // 이 플레이어에게만 오디오 리스너를 부착
        //    spawnedPlayerPrefab.AddComponent<AudioListener>();
        //}
        //else
        //{
        //    // 다른 플레이어에게 오디오 리스너를 부착하지 않음
        //    Destroy(GetComponent<AudioListener>());
        //}
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        //spawnedPlayerPrefab = PhotonNetwork.Instantiate("XR Interaction Hands Setup Variant", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
