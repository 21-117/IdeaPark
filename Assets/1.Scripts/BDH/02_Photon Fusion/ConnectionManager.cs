using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    // Fusion ��Ʈ��ũ ������ �� �̺�Ʈ �ڵ鷯
    private NetworkRunner networkRunner;
    private NetworkEvents networkEvents;

    // ���� ������ ã�� ��Ʈ��ũ ������Ʈ �迭
    private NetworkObject[] networkObjectArray;

    private void Awake()
    {
        // ��Ʈ��ũ ������Ʈ �迭 �ʱ�ȭ
        networkObjectArray = GetNetworkObjects();

        // Fusion ��Ʈ��ũ ������ �� �̺�Ʈ �ڵ鷯 ��������
        networkRunner = GetComponent<NetworkRunner>();
        networkEvents = GetComponent<NetworkEvents>();

        // ������ ����Ǿ��� ���� �̺�Ʈ�� RegisterNetworkObjects �޼��� �߰�
        networkEvents.OnConnectedToServer.AddListener(RegisterNetworkObjects);
    }

    // ��ü�� �ı��� �� ��ϵ� �̺�Ʈ �ڵ鷯 ����
    private void OnDestroy() => networkEvents.OnConnectedToServer.RemoveListener(RegisterNetworkObjects);

    private void Start()
    {
        // ���� ���� �� Fusion ��Ʈ��ũ �ʱ�ȭ
        networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = SceneManager.GetActiveScene().buildIndex.ToString()
        });
    }

    // ������ ��Ʈ��ũ ������Ʈ�� ã�� �迭�� ��ȯ�ϴ� �޼���
    private NetworkObject[] GetNetworkObjects() => FindObjectsByType<NetworkObject>(FindObjectsSortMode.None);

    // ��Ʈ��ũ ������Ʈ �迭�� Fusion ��Ʈ��ũ �����ڿ� ����ϴ� �޼���
    private void RegisterNetworkObjects(NetworkRunner runner) => runner.RegisterSceneObjects(networkObjectArray);   
}
