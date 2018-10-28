using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkBehaviourManager : NetworkBehaviour
{

    public override void OnStartServer()
    {
        tools.instance.ImServer = true;
        Debug.Log("I'm server !");
    }
    public override void OnStartClient()
    {
        tools.instance.ImServer = false;
        tools.instance.ImConnected = true;
        Debug.Log("I'm Client CONNECTED !");
    }
}
