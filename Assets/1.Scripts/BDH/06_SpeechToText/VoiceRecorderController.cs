//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System; 

//// ����� ���̽� ê ����� Ȱ��ȭ �� ��Ȱ��ȭ ���.
//public class VoiceRecorderController : MonoBehaviour
//{
//    private Recorder recorder;

//    // ����� ���̽� ê ����� Ȱ��ȭ
//    public static Action onStartTransmit;

//    // ����� ���̽� ê ����� �� Ȱ��ȭ
//    public static Action onStopTransmit;

//    private void Awake()
//    {
//        recorder = GetComponent<Recorder>();    
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        // �ʱⰪ���δ� ��Ȱ��ȭ�� ����. 
//        recorder.TransmitEnabled = false; 

//        onStartTransmit = () =>
//        {
//            OnStartTransmitEnabled();
//        };

//        onStopTransmit = () =>
//        {
//            OnStopTransmitEnabled();
//        };
//    }

//    public void OnStartTransmitEnabled()
//    {
//        recorder.TransmitEnabled = true;
//    }

//    public void OnStopTransmitEnabled()
//    {
//        recorder.TransmitEnabled = false; 
//    }

//}
