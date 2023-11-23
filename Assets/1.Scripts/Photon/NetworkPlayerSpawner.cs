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

        // 초기에 스폰 위치 큐를 초기화합니다.
        //InitializeSpawnPointsQueue();
        xrOrigin = GameObject.Find("XR Interaction Hands Setup Variant");

    }
    void InitializeSpawnPointsQueue()
    {
        // 초기화할 때마다 스폰 위치 큐를 채웁니다.
        foreach (Transform point in spawnPoints)
        {
            spawnPointsQueue.Enqueue(point);
        }
    }

    Transform spawnPoint;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        //// 큐에서 다음 스폰 위치를 가져옵니다.
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
