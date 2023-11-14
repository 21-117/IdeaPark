using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private static Queue<Transform> spawnPointsQueue = new Queue<Transform>();
    public Transform[] spawnPoints;

    void Start()
    {

        // �ʱ⿡ ���� ��ġ ť�� �ʱ�ȭ�մϴ�.
        InitializeSpawnPointsQueue();
   

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

        // ť���� ���� ���� ��ġ�� �����ɴϴ�.
        if (spawnPointsQueue.Count > 0)
        {
            spawnPoint = spawnPointsQueue.Dequeue();
        }
        else
        {
            Debug.LogError("No more spawn points available!");
        }

        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", spawnPoint.position, spawnPoint.rotation);
        //spawnedPlayerPrefab.GetComponent<NewtworkPlayer>().head.
        //spawnedPlayerPrefab = PhotonNetwork.Instantiate("XR Interaction Hands Setup Variant", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
