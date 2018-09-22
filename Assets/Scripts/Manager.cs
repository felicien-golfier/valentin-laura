using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Manager : NetworkManager
{
    private void Start()
    {
        Debug.Log("toto");
    }

    public override void OnStartHost()
    {
        Debug.LogWarning("OnStartHost");
    }

}