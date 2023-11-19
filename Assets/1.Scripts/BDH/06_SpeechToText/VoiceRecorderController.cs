//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System; 

//// 사용자 보이스 챗 기능을 활성화 및 비활성화 기능.
//public class VoiceRecorderController : MonoBehaviour
//{
//    private Recorder recorder;

//    // 사용자 보이스 챗 기능을 활성화
//    public static Action onStartTransmit;

//    // 사용자 보이스 챗 기능을 비 활성화
//    public static Action onStopTransmit;

//    private void Awake()
//    {
//        recorder = GetComponent<Recorder>();    
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        // 초기값으로는 비활성화로 설정. 
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
