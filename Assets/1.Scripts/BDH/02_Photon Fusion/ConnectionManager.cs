using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    // Fusion 네트워크 관리자 및 이벤트 핸들러
    private NetworkRunner networkRunner;
    private NetworkEvents networkEvents;

    // 현재 씬에서 찾은 네트워크 오브젝트 배열
    private NetworkObject[] networkObjectArray;

    private void Awake()
    {
        // 네트워크 오브젝트 배열 초기화
        networkObjectArray = GetNetworkObjects();

        // Fusion 네트워크 관리자 및 이벤트 핸들러 가져오기
        networkRunner = GetComponent<NetworkRunner>();
        networkEvents = GetComponent<NetworkEvents>();

        // 서버에 연결되었을 때의 이벤트에 RegisterNetworkObjects 메서드 추가
        networkEvents.OnConnectedToServer.AddListener(RegisterNetworkObjects);
    }

    // 객체가 파괴될 때 등록된 이벤트 핸들러 제거
    private void OnDestroy() => networkEvents.OnConnectedToServer.RemoveListener(RegisterNetworkObjects);

    private void Start()
    {
        // 게임 시작 시 Fusion 네트워크 초기화
        networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = SceneManager.GetActiveScene().buildIndex.ToString()
        });
    }

    // 씬에서 네트워크 오브젝트를 찾아 배열로 반환하는 메서드
    private NetworkObject[] GetNetworkObjects() => FindObjectsByType<NetworkObject>(FindObjectsSortMode.None);

    // 네트워크 오브젝트 배열을 Fusion 네트워크 관리자에 등록하는 메서드
    private void RegisterNetworkObjects(NetworkRunner runner) => runner.RegisterSceneObjects(networkObjectArray);   
}
