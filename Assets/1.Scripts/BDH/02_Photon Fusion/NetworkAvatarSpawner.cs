using UnityEngine;
using Fusion;
using System;

public class NetworkAvatarSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NetworkRunner networkRunner;
    [SerializeField] private NetworkEvents networkEvents;
    [SerializeField] private UserEntitlement userEntitlement;

    [Header("Prefabs")]
    [SerializeField] private NetworkObject avatarPrefab;

    [Header("Ovr Rig")]
    [SerializeField] private Transform cameraRigTransform;


    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    private bool isServerConnected = false;
    private bool isEntitlementGranted = false;

    private void Awake()
    {
        // 서버에 연결되었을 때와 권한이 부여되었을 때의 이벤트 핸들러 등록
        networkEvents.OnConnectedToServer.AddListener(ConnectedToServer);
        userEntitlement.OnEntitlementGranted += EntintlementGranted;

    }

    private void OnDestroy()
    {
        // 이벤트 핸들러 제거
        networkEvents.OnConnectedToServer.RemoveListener(ConnectedToServer);
        userEntitlement.OnEntitlementGranted -= EntintlementGranted;
    }
    private void EntintlementGranted()
    {
        // 권한 부여 이벤트 발생 시 호출되는 메서드
        isEntitlementGranted = true;
        TrySpawnAvatar();
    }

    private void ConnectedToServer(NetworkRunner arg0)
    {
        // 서버에 연결되었을 때 호출되는 메서드
        isServerConnected = true;
        TrySpawnAvatar();
    }

    private void TrySpawnAvatar()
    {
        // 서버 연결 상태와 권한 부여 상태를 확인하고 아바타 스폰 시도
        if (!isServerConnected || !isEntitlementGranted) return;

        SetPlayerSpawnPosition();
        SpawnAvatar();
    }

    private void SetPlayerSpawnPosition()
    {
        // 플레이어 스폰 위치 설정 메서드
        Vector3 boxSize = new(1, 1, 1);

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            // 박스 형태의 충돌체로 플레이어 스폰 지점 확인
            bool isOccupied = Physics.CheckBox(spawnPoints[i].position, boxSize, spawnPoints[i].rotation, LayerMask.GetMask("Player"));

            if(!isOccupied)
            {
                // 빈 스폰 지점이면 Ovr Rig 위치 설정
                cameraRigTransform.SetPositionAndRotation(spawnPoints[i].position, spawnPoints[i].rotation);
                break;
            }
        }
    }

    private void SpawnAvatar()
    {
        // 아바타 스폰 메서드
        var avatar = networkRunner.Spawn(avatarPrefab, cameraRigTransform.position, cameraRigTransform.rotation, networkRunner.LocalPlayer);
        avatar.transform.SetParent(cameraRigTransform); 
    }
}
