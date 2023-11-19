//using Photon.Pun;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.XR.CoreUtils;
//using UnityEngine;
//using UnityEngine.XR;
//using UnityEngine.XR.Interaction.Toolkit;

//public class NewtworkPlayer : MonoBehaviour
//{
//    public Transform head, audioListnerObj;
//    public Transform leftHand;
//    public Transform rightHand;
//    private PhotonView photonView;

//    private Transform headRig;
//    private Transform leftHandRig;
//    private Transform rightHandRig;

//    public List<Material> bodyMats;

//    private AudioListener audioListener;
//    // Start is called before the first frame update
//    void Start()
//    {
//        photonView = GetComponent<PhotonView>();
//        XROrigin rig = FindObjectOfType<XROrigin>();
//        headRig = rig.transform.Find("Camera Offset/Main Camera");
//        leftHandRig = rig.transform.Find("Camera Offset/Left Hand/Left Hand Interaction Visual/L_Wrist");
//        rightHandRig = rig.transform.Find("Camera Offset/Right Hand/Right Hand Interaction Visual/R_Wrist");

//        audioListener = audioListnerObj.GetComponent<AudioListener>();


//        if (photonView.IsMine)
//        {
//            if (audioListener != null)
//            {
//                audioListnerObj.gameObject.SetActive(true);
//            }
//            foreach(var item in GetComponentsInChildren<SkinnedMeshRenderer>())
//            {
//                item.enabled = false;
//            }

//            bodyMats.Add(PlayerInfo.localPlayer.head.material);
//            bodyMats.Add(PlayerInfo.localPlayer.leftHand.material);
//            bodyMats.Add(PlayerInfo.localPlayer.rightHand.material);
//        }
//        else
//        {
//            if (audioListener != null)
//            {
//                audioListnerObj.gameObject.SetActive(false);
//            }
//        }
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        if (photonView.IsMine)
//        {
//            MapPosition(head, headRig);
//            MapPosition(leftHand, leftHandRig);
//            MapPosition(rightHand, rightHandRig);
//        }

//    }

//    void MapPosition(Transform target, Transform rigTransform)
//    {

//        target.position = rigTransform.position;
//        target.rotation = rigTransform.rotation;
//    }
//}
