using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Image url�� �ݿ��ϴ� Image UI 
    public Image downLoadImage;

    // Todos_List UI ��ư Ŭ�� ������ -> RequestType.GET
    public void OnClickTodos()
    {
        HttpInfo info = new HttpInfo();
        info.SetInfo(RequestType.GET, "/todos", (DownloadHandler downLoadHandler) => {
            print("todos : " + downLoadHandler.text);
        });

        HttpManager.Get().SendRequest(info);

    }

    // Comments UI ��ư Ŭ�� ������ -> RequestType.GET
    public List<CommentInfo> comments;
    public void OnClickComments()
    {
        HttpInfo info = new HttpInfo();
        info.SetInfo(RequestType.GET, "/comments", (DownloadHandler downLoadHandler) =>
        {
            print("comments : " + downLoadHandler.text);
            string jsonData = "{\"data\" : " + downLoadHandler.text + "}";

            //���� ���� jsonData �� ������ parsing 
            JsonList<CommentInfo> commentList = JsonUtility.FromJson<JsonList<CommentInfo>>(jsonData);

            comments = commentList.data;
        });

        HttpManager.Get().SendRequest(info);
    }

    // ȸ������ SignUp UI ��ư Ŭ�� ������ -> RequestType.POST
    public void PostTest()
    {
        HttpInfo info = new HttpInfo();
        info.SetInfo(RequestType.POST, "/create/signUp", (DownloadHandler downLoadHandler) =>
        {
            // Post ������ �������� �� �����κ��� ����.
        });

        SignUpInfo signUpInfo = new SignUpInfo();
        signUpInfo.userName = "test";
        signUpInfo.birthday = "test";
        signUpInfo.age = 22;


        info.body = JsonUtility.ToJson(signUpInfo);

        HttpManager.Get().SendRequest(info);
    }

    // ImageDownLoad UI ��ư Ŭ�� ������ -> RequestType.GET
    public void OnClickDownLoadImage()
    {
        //https://via.placeholder.com/150/f9cee5

        HttpInfo info = new HttpInfo();
        info.SetInfo(RequestType.TEXTURE, "https://via.placeholder.com/150/f9cee5", (DownloadHandler downloadHandler) =>
        {
            // �ٿ�ε�� Image �����͸� Sprite�� �����. 
            // Texture2D - > Sprite
            Texture2D texture = ((DownloadHandlerTexture)downloadHandler).texture;
            downLoadImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }, false);
        
        HttpManager.Get().SendRequest(info);
    }

}
