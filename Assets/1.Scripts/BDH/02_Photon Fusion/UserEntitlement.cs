using Oculus.Avatar2;
using Oculus.Platform;
using Oculus.Platform.Models;
using Oculus.Platform.Samples.EntitlementCheck;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserEntitlement : MonoBehaviour
{
    public static ulong OculusID;

    public Action OnEntitlementGranted;

    private void Awake() => EntitlementCheck();

    private void EntitlementCheck()
    {
        try
        {
            Core.AsyncInitialize();
            Entitlements.IsUserEntitledToApplication().OnComplete(IsUserEntitledToApplicationCommplete); 
        }catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void IsUserEntitledToApplicationCommplete(Message message)
    {
        if(message.IsError)
        {
            Debug.LogError(message.GetError());
            return;
        }

        Users.GetAccessToken().OnComplete(GetAccessTokenComplete);
    }

    private void GetAccessTokenComplete(Message<string> message)
    {
        if(message.IsError)
        {
            Debug.LogError(message.GetError());
            return;
        }

        OvrAvatarEntitlement.SetAccessToken(message.Data);

        Users.GetLoggedInUser().OnComplete(GetLoggedInUserComplete);
    }

    private void GetLoggedInUserComplete(Message<User> message)
    {
        if (message.IsError)
        {
            Debug.LogError(message.GetError());
            return;
        }
        OculusID = message.Data.ID;
        OnEntitlementGranted?.Invoke(); 
    }
}
