using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkBehaviourManager : NetworkBehaviour
{
    private NetworkIdentity nid;
    private void Awake()
    {
        nid = GetComponent<NetworkIdentity>();
    }


    [Command]
    public void CmdNewClient(string nfc_id)
    {
        Tools.instance.serverLaura.NewClient(nfc_id);
    }

    [Command]
    public void CmdAddBet(string nfc_id, int number)
    {
        Tools.instance.serverLaura.AddBet(nfc_id, number);
    }

    [Command]
    public void CmdGetClientToStore(string id)
    {
        Tools.instance.serverLaura.GetClientToStore(id);
    }

    [ClientRpc]
    public void RpcSendClient(string nfc_id, int nbRemainingBet, float gains)
    {
        if (!isServer)
        {
            Debug.Log("Got Client !!");
            Tools.instance.store.UpdateMyClient(nfc_id, nbRemainingBet, gains);
        }
    }

    public void ConnectedToServer(bool connexion)
    {
        nid.localPlayerAuthority = connexion;
    }

    public override void OnStartServer()
    {
        Tools.instance.ImServer = true;
        ConnectedToServer(false);
        Debug.Log("I'm server !");
    }
    public override void OnStartClient()
    {
        if (!isServer && !Tools.instance.ImConnected)
        {
            Tools.instance.ImServer = false;
            Tools.instance.ImConnected = true;
            ConnectedToServer(true);
            Debug.Log("I'm Client CONNECTED !");
            Tools.DisplayPopup(true,"Connected");
        }
    }

    public override void OnStartLocalPlayer()
    {
        Tools.instance.netManager = this;
        Debug.Log("OnStartLocalPlayer!");
    }
}