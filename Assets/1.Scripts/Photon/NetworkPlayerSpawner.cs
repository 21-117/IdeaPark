using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab, xrOrigin;
    private static Queue<Transform> spawnPointsQueue = new Queue<Transform>();
    public Transform[] spawnPoints;

    void Start()
    {

        // �ʱ⿡ ���� ��ġ ť�� �ʱ�ȭ�մϴ�.
        //InitializeSpawnPointsQueue();
        xrOrigin = GameObject.Find("XR Interaction Hands Setup Variant");

    }
    void InitializeSpawnPointsQueue()
    {
        // �ʱ�ȭ�� ������ ���� ��ġ ť�� ä��ϴ�.
        foreach (Transform point in spawnPoints)
        {
            spawnPointsQueue.Enqueue(point);
        }
    }

    Transform spawnPoint;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        //// ť���� ���� ���� ��ġ�� �����ɴϴ�.
        //if (spawnPointsQueue.Count > 0)
        //{
        //    spawnPoint = spawnPointsQueue.Dequeue();
        //}
        //else
        //{
        //    Debug.LogError("No more spawn points available!");
        //}

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        xrOrigin.transform.position = spawnPoints[playerCount - 1].position;
        xrOrigin.transform.rotation = spawnPoints[playerCount - 1].rotation;
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", spawnPoints[playerCount-1].position, spawnPoints[playerCount-1].rotation);
        
        
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
