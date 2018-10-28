#define SERVER
//#define CLIENT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNet : MonoBehaviour {

    public bool ImServer;
    NetworkManager manager;

    private void Awake()
    {
        this.manager = this.GetComponent<NetworkManager>();
    }

    void Start () {
        if (ImServer)
            manager.StartServer();
        else
            StartClient();
    }

    public void StartClient()
    {
        manager.StartClient();
    }

    public void SetupServer()
    {
        NetworkServer.Listen(4444);
    }

}