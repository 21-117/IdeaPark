using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Image url�� �ݿ��ϴ� Image UI 
    public Image downLoadImage;

    // http://192.168.0.2:8000/get_data
    public void OnClickBNSERVER()
    {
        HttpInfo info = new HttpInfo();
        info.SetInfo(RequestType.GET, "/get_data", (DownloadHandler downLoadHandler) => {
            print("/get_data : " + downLoadHandler.text);
        });

        HttpManager.Get().SendRequest(info);

    }



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
    // http://192.168.0.2:8000/save_data/
    public void PostTest()
    {
        HttpInfo info = new HttpInfo();
        info.SetInfo(RequestType.POST, "/save_data/", (DownloadHandler downLoadHandler) =>
        {
            // Post ������ �������� �� �����κ��� ����.
        });

        //SignUpInfo signUpInfo = new SignUpInfo();
        //signUpInfo.userName = "test";
        //signUpInfo.birthday = "test";
        //signUpInfo.age = 22;

        NodeData textNode = new NodeData();
        textNode.id = 7;
        textNode.Node_Text = "test";
        textNode.Node_type = 2;
        textNode.aiData = null; 
        textNode.isSelected = true;
        textNode.Children = null; 



        info.body = JsonUtility.ToJson(textNode);

        HttpManager.Get().SendRequest(info);
    }

    // http://192.168.0.2:8000/keyword-suggestions/
    public void PostAiTest()
    {
        HttpInfo info = new HttpInfo();
        info.SetInfo(RequestType.POST, "/keyword-suggestions/", (DownloadHandler downLoadHandler) =>
        {
            // Post ������ �������� �� �����κ��� ����.
        });



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
