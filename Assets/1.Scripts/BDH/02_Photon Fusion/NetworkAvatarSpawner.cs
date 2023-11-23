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
        // ������ ����Ǿ��� ���� ������ �ο��Ǿ��� ���� �̺�Ʈ �ڵ鷯 ���
        networkEvents.OnConnectedToServer.AddListener(ConnectedToServer);
        userEntitlement.OnEntitlementGranted += EntintlementGranted;

    }

    private void OnDestroy()
    {
        // �̺�Ʈ �ڵ鷯 ����
        networkEvents.OnConnectedToServer.RemoveListener(ConnectedToServer);
        userEntitlement.OnEntitlementGranted -= EntintlementGranted;
    }
    private void EntintlementGranted()
    {
        // ���� �ο� �̺�Ʈ �߻� �� ȣ��Ǵ� �޼���
        isEntitlementGranted = true;
        TrySpawnAvatar();
    }

    private void ConnectedToServer(NetworkRunner arg0)
    {
        // ������ ����Ǿ��� �� ȣ��Ǵ� �޼���
        isServerConnected = true;
        TrySpawnAvatar();
    }

    private void TrySpawnAvatar()
    {
        // ���� ���� ���¿� ���� �ο� ���¸� Ȯ���ϰ� �ƹ�Ÿ ���� �õ�
        if (!isServerConnected || !isEntitlementGranted) return;

        SetPlayerSpawnPosition();
        SpawnAvatar();
    }

    private void SetPlayerSpawnPosition()
    {
        // �÷��̾� ���� ��ġ ���� �޼���
        Vector3 boxSize = new(1, 1, 1);

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            // �ڽ� ������ �浹ü�� �÷��̾� ���� ���� Ȯ��
            bool isOccupied = Physics.CheckBox(spawnPoints[i].position, boxSize, spawnPoints[i].rotation, LayerMask.GetMask("Player"));

            if(!isOccupied)
            {
                // �� ���� �����̸� Ovr Rig ��ġ ����
                cameraRigTransform.SetPositionAndRotation(spawnPoints[i].position, spawnPoints[i].rotation);
                break;
            }
        }
    }

    private void SpawnAvatar()
    {
        // �ƹ�Ÿ ���� �޼���
        var avatar = networkRunner.Spawn(avatarPrefab, cameraRigTransform.position, cameraRigTransform.rotation, networkRunner.LocalPlayer);
        avatar.transform.SetParent(cameraRigTransform); 
    }
}
