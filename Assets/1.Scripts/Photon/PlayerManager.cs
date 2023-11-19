//using UnityEngine;
//using Photon.Pun;

//public class PlayerManager : MonoBehaviourPunCallbacks
//{
//    void Start()
//    {
//        if (photonView.IsMine)
//        {
//            // 로컬 플레이어일 경우 실행할 코드 작성
//        }
//        else
//        {
//            // 원격 플레이어일 경우 실행할 코드 작성
//        }
//    }

//    void Update()
//    {
//        if (photonView.IsMine)
//        {
//            // 로컬 플레이어의 입력 처리 및 게임 로직 작성
//        }
//        else
//        {
//            // 원격 플레이어의 위치 동기화 처리
//            //transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
//        }
//    }

//    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if (stream.IsWriting)
//        {
//            // 로컬 플레이어의 데이터를 스트림에 쓰기
//            stream.SendNext(transform.position);
//        }
//        else
//        {
//            // 원격 플레이어의 데이터를 스트림에서 읽기
//            //correctPlayerPos = (Vector3)stream.ReceiveNext();
//        }
//    }
//}
