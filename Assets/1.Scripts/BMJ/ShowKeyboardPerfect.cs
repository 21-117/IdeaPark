using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowKeyboardPerfect : MonoBehaviour
{
    //TMPRo Inputfield�� private���� �Ľ��Ѵ�.
    private TMP_InputField inputField;

    //������ �Ÿ�
    public float distance = 0.5f;

    //���� �Ÿ�
    public float verticalOffset = -0.5f;

    //ī�޶��� ��ġ��
    public Transform cameraPos;

    private void Start()
    {
        //inputfield ������Ʈ�� �����´�.
        inputField = GetComponent<TMP_InputField>();
        //inputfield�� �����ؼ� �߰��Ѵ�. x�� ������ inputfield�� �ǹ��Ѵ�.
        /*x�� ���� ���� OnSelect �̺�Ʈ�� ���� ���޵� ���ڸ� �ޱ� ���� ���̰�,
         * ������ ������ ������ �̺�Ʈ �ڵ鷯�� �ñ״�ó�� ��ġ�ϱ� ���� ���ԵǾ� �ִ� */
        inputField.onSelect.AddListener(x => ShowKeyboard());
    }

    //Ű���带 �����ش�.
    public void ShowKeyboard()
    {
        //�� Ű������ inputfield�� ���� ������ inputField�� ��ü�Ѵ�.
        NonNativeKeyboard.Instance.InputField = inputField;
        //���� �Է��� Ű���带 �Է��Ѵ�.
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        //ī�޶��� �� ������ ���Ѵ�.
        Vector3 cameraForward = cameraPos.forward;
        //y�� ���� 0���� ����� ����ȭ�Ѵ�.
        cameraForward.y = 0;
        cameraForward.Normalize();

        //���� ī�޶� ���� ���� �� �ؿ� ��Ÿ���� �����.
        Vector3 setKeyboardPos = cameraPos.position + cameraForward * distance + Vector3.up * verticalOffset;
        //Ű���忡 ��ġ���� �ִ´�.
        NonNativeKeyboard.Instance.RepositionKeyboard(setKeyboardPos);

        SetCaretColorAlpha(1);

        //�ݾ��� �� ����� �߰��Ѵ�.
        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }

    //Ű���尡 ���� �� ȣ��Ǵ� �Լ�
    private void Instance_OnClosed(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(0);
        //������ �� ����� �߰��Ѵ�.
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    //Ŀ���� ���İ��� �����ϰ� �ʹ�.
    public void SetCaretColorAlpha(float alpha)
    {
        //Ŀ�� Ŀ���� ���� ���� �� �� �ְ� �����.
        inputField.customCaretColor = true;
        //inputfield�� ������ �����´�.
        Color caretColor = inputField.caretColor;
        //���� ������ ������ alpha�� �Ѵ�.
        caretColor.a = alpha;
        //inputfield�� ������ �ٲ۴�.
        inputField.caretColor = caretColor;
    }
}