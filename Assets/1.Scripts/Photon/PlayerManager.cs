//using UnityEngine;
//using Photon.Pun;

//public class PlayerManager : MonoBehaviourPunCallbacks
//{
//    void Start()
//    {
//        if (photonView.IsMine)
//        {
//            // ���� �÷��̾��� ��� ������ �ڵ� �ۼ�
//        }
//        else
//        {
//            // ���� �÷��̾��� ��� ������ �ڵ� �ۼ�
//        }
//    }

//    void Update()
//    {
//        if (photonView.IsMine)
//        {
//            // ���� �÷��̾��� �Է� ó�� �� ���� ���� �ۼ�
//        }
//        else
//        {
//            // ���� �÷��̾��� ��ġ ����ȭ ó��
//            //transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
//        }
//    }

//    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if (stream.IsWriting)
//        {
//            // ���� �÷��̾��� �����͸� ��Ʈ���� ����
//            stream.SendNext(transform.position);
//        }
//        else
//        {
//            // ���� �÷��̾��� �����͸� ��Ʈ������ �б�
//            //correctPlayerPos = (Vector3)stream.ReceiveNext();
//        }
//    }
//}
