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
        //    // �� �÷��̾�Ը� ����� �����ʸ� ����
        //    spawnedPlayerPrefab.AddComponent<AudioListener>();
        //}
        //else
        //{
        //    // �ٸ� �÷��̾�� ����� �����ʸ� �������� ����
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
