using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// https://jsonplaceholder.typicode.com

// JSONLIST KEY 형식. 
[Serializable]
public class JsonList<T>
{
    public List<T> data; 
}

// Comments
[Serializable]
public struct CommentInfo
{
    public int postId;
    public int id;
    public string name;
    public string email;
    public string body;
}

[Serializable]
public struct SignUpInfo
{
    public string userName;
    public string birthday;
    public int age; 
}

// Http RestAPI GET, POST, PUT, DELETE, TEXTURE 분기  
public enum RequestType
{
    GET,
    POST,
    PUT,
    DELETE,
    TEXTURE
}

// 웹 서버와 통신하기 위한 HttpInfo 정보 
public class HttpInfo
{
    public RequestType requestType; 
    public string url = "";
    // Post 통신 PostWwwForm 형태로 구성된 body .  
    public string body; 
    public Action<DownloadHandler> onReceiveData;

    public void SetInfo(RequestType type, 
        string u, 
        Action<DownloadHandler> callback, 
        bool useDefaultUrl = true)
    {
        requestType = type;
        if (useDefaultUrl) url = "https://jsonplaceholder.typicode.com";
        url += u;

        onReceiveData = callback;   
    }
}

public class HttpManager : MonoBehaviour
{
    static HttpManager instance;

    public static HttpManager Get()
    {
        if (instance == null)
        {
            
            GameObject go = new GameObject("HttpStudy");
          
            go.AddComponent<HttpManager>();
        }

        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //서버에게 REST API 요청 (GET, POST, PUT, DELETE)
    public void SendRequest(HttpInfo httpInfo)
    {
        StartCoroutine(CoSendRequest(httpInfo));
    }

    IEnumerator CoSendRequest(HttpInfo httpInfo)
    {
        UnityWebRequest req = null;

        // POST, GET, PUT, DELETE, TEXTURE 
        switch (httpInfo.requestType)
        {
            case RequestType.GET:
                req = UnityWebRequest.Get(httpInfo.url);
                break;
            case RequestType.POST:
                req = UnityWebRequest.PostWwwForm(httpInfo.url, httpInfo.body);
                byte[] byteBody = Encoding.UTF8.GetBytes(httpInfo.body);
                req.uploadHandler = new UploadHandlerRaw(byteBody);
                // 헤더 추가
                req.SetRequestHeader("Content-Type", "application/json");

                break;
            case RequestType.PUT:
                req = UnityWebRequest.Put(httpInfo.url, "");
                break;
            case RequestType.DELETE:
                req = UnityWebRequest.Delete(httpInfo.url); 
                break;
            case RequestType.TEXTURE:
                req = UnityWebRequestTexture.GetTexture(httpInfo.url);
                break;
            default:
                break;
        }

       
        // 서버에 요청이 응답할 때까지 대기 
        yield return req.SendWebRequest();

        // 서버에서 요청 성공,  
        if (req.result == UnityWebRequest.Result.Success)
        {
           
            if(httpInfo.onReceiveData != null)
            {
           
                httpInfo.onReceiveData(req.downloadHandler); 
            }
           
        }
        //요청 실패 
        else
        {
            print("요청 실패 : " + req.error);
        }
    }



}
