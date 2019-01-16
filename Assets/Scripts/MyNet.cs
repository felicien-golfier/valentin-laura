#define SERVER
//#define CLIENT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNet : NetworkDiscovery
{
    NetworkManager manager;
 
    private void Awake()
    {
        this.manager = this.GetComponent<NetworkManager>();
    }

    void Start () {
            this.Initialize();
        if (Tools.instance.ImServer)
        {
            manager.StartHost();
            this.StartAsServer();
        }else
        {
            this.StartAsClient();
        }
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (!Tools.instance.ImServer && !Tools.instance.ImConnected)
        {
            manager.networkAddress = fromAddress;
            manager.StartClient();
        }
    }
}