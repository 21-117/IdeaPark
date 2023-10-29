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
        // Photon ȯ�� ������ ������� ������ ������ ������ �õ�
        PhotonNetwork.ConnectUsingSettings(); 
    }

    // ������ ���� ���� �Ϸ�.
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        // 1.������ ���� ���ӿ� �����ϸ� ��µȴ�. 
        print(nameof(OnConnectedToMaster));

        // 2. �κ� ����. 
        joinLobby();
        
        // 3. �κ� ���� �Ϸ����� �� Room ����
    }

    // �κ� �����ϴ� �޼ҵ� 
    private void joinLobby()
    {
        // �г��� ���� 
        PhotonNetwork.NickName = "����ȯ";
        // �⺻ Lobby�� ����.
        PhotonNetwork.JoinLobby();
    }

    // �κ� ���� �Ϸ�� Room ���� ���� 
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(nameof(OnJoinedLobby));

        // �� ���� �� ����
        RoomOptions roomOption = new RoomOptions();
        // Room �ɼ� ���� ���Ѵ� => ����� �⺻ �ɼ����� ����. ( isOpen, isVisible,.... ) 

        PhotonNetwork.JoinOrCreateRoom("�� ���� : IdeaPark", roomOption, TypedLobby.Default); 
    }

    // �� ���� �Ϸ� �� ȣ��Ǵ� �Լ�. 
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(nameof(OnCreatedRoom));


    }

    // �� ���� ���� �� ȣ��Ǵ� �Լ�. 
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        print(nameof(OnCreateRoomFailed));

        // �� ���� ������ �����ִ� �˾�
    }

    // �� ���� ���� �� ȣ��Ǵ� �Լ�.
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(nameof(OnJoinedRoom));

        // �κ� ���� -> �� ���� / ���Ա��� �Ϸ��� ���� 
        // GameScene���� �� ��ȯ. 
        //PhotonNetwork.LoadLevel("GameScene");

    }
}

