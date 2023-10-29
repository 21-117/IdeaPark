using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIButtonManager : MonoBehaviour
{
    //버튼 프리펩을 가져온다.
    public GameObject AIButton;

    //추가할 부모 Transform을 가져온다.
    public Transform parent;

    //5글자 이상인 음식과 관련된 예제 리스트 5개를 만들어보겠다.
    private List<string> data = new List<string>()
    {
        "피자 냠냠",
        "치킨 먹자",
        "햄버거 맛있다",
        "족발 세트",
        "족발 세트",
        "족발 세트",
        "족발 세트",
        "족발 세트",
        "족발 세트",
        "원할머니 보쌈"
    };

    private void Start()
    {
        //Start 대신에 버튼 클릭하실 때 사용하시면 됩니다 !
        CreateAIKeyword();
    }

    private void CreateAIKeyword()
    {
        //부모를 지정해서 Prefab을 생성한다.
        for (int i = 0; i < data.Count; i++)
        {
            GameObject button = Instantiate(AIButton, parent);
            //button 프리팹에 있는 버튼의 Text 부분에 하나씩 넣는다.
            button.GetComponentInChildren<TextMeshProUGUI>().text = data[i];
        }
    }
}