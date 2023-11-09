using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowKeyboardPerfect : MonoBehaviour
{
    //TMPRo Inputfield를 private으로 파싱한다.
    private TMP_InputField inputField;

    //나와의 거리
    public float distance = 0.5f;

    //수직 거리
    public float verticalOffset = -0.5f;

    //카메라의 위치값
    public Transform cameraPos;

    private void Start()
    {
        //inputfield 컴포넌트를 가져온다.
        inputField = GetComponent<TMP_InputField>();
        //inputfield를 선택해서 추가한다. x는 선택한 inputfield를 의미한다.
        /*x를 쓰는 이유 OnSelect 이벤트에 의해 전달된 인자를 받기 위한 것이고,
         * 실제로 사용되지 않지만 이벤트 핸들러의 시그니처와 일치하기 위해 포함되어 있다 */
        inputField.onSelect.AddListener(x => ShowKeyboard());
    }

    //키보드를 보여준다.
    public void ShowKeyboard()
    {
        //이 키보드의 inputfield를 내가 선택한 inputField로 교체한다.
        NonNativeKeyboard.Instance.InputField = inputField;
        //내가 입력한 키보드를 입력한다.
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        //카메라의 앞 방향을 구한다.
        Vector3 cameraForward = cameraPos.forward;
        //y축 값을 0으로 만들고 정규화한다.
        cameraForward.y = 0;
        cameraForward.Normalize();

        //내가 카메라 앞을 봤을 때 밑에 나타나게 만든다.
        Vector3 setKeyboardPos = cameraPos.position + cameraForward * distance + Vector3.up * verticalOffset;
        //키보드에 위치값을 넣는다.
        NonNativeKeyboard.Instance.RepositionKeyboard(setKeyboardPos);

        SetCaretColorAlpha(1);

        //닫았을 때 기능을 추가한다.
        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }

    //키보드가 닫을 때 호출되는 함수
    private void Instance_OnClosed(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(0);
        //열었을 때 기능을 추가한다.
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    //커서의 알파값을 조정하고 싶다.
    public void SetCaretColorAlpha(float alpha)
    {
        //커서 커스텀 색상 값을 할 수 있게 만든다.
        inputField.customCaretColor = true;
        //inputfield의 색상을 가져온다.
        Color caretColor = inputField.caretColor;
        //내가 지정한 색상을 alpha로 한다.
        caretColor.a = alpha;
        //inputfield의 색상을 바꾼다.
        inputField.caretColor = caretColor;
    }
}