using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        // Photon 환경 설정을 기반으로 마스터 서버에 접속을 시도
        PhotonNetwork.ConnectUsingSettings(); 
    }

    // 마스터 서버 접속 완료.
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        // 1.마스터 서버 접속에 성공하면 출력된다. 
        print(nameof(OnConnectedToMaster));

        // 2. 로비 진입. 
        joinLobby();
        
        // 3. 로비 진입 완료했을 때 Room 생성
    }

    // 로비 진입하는 메소드 
    private void joinLobby()
    {
        // 닉네임 설정 
        PhotonNetwork.NickName = "변동환";
        // 기본 Lobby에 입장.
        PhotonNetwork.JoinLobby();
    }

    // 로비 진입 완료시 Room 생성 가능 
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(nameof(OnJoinedLobby));

        // 방 생성 및 참여
        RoomOptions roomOption = new RoomOptions();
        // Room 옵션 값을 정한다 => 현재는 기본 옵션으로 설정. ( isOpen, isVisible,.... ) 

        PhotonNetwork.JoinOrCreateRoom("방 제목 : IdeaPark", roomOption, TypedLobby.Default); 
    }

    // 방 생성 완료 시 호출되는 함수. 
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(nameof(OnCreatedRoom));


    }

    // 방 생성 실패 시 호출되는 함수. 
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        print(nameof(OnCreateRoomFailed));

        // 방 실패 원인을 보여주는 팝업
    }

    // 방 참여 성공 시 호출되는 함수.
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(nameof(OnJoinedRoom));

        // 로비 진입 -> 방 생성 / 진입까지 완료한 상태 
        // GameScene으로 씬 전환. 
        //PhotonNetwork.LoadLevel("GameScene");

    }
}

