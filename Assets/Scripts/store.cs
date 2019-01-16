using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Store : MonoBehaviour {

    public ClientToDiplay myClient;
    private int laura_id;
    private ClientLaura _client;
    private string lastUdpPacket;

    private void Update()
    {
        string  udp = Tools.instance.udp.getLatestUDPPacket();
        if (udp != "" && udp != lastUdpPacket)
        {
            lastUdpPacket = udp;
            ReadNFCID(lastUdpPacket);
        }
    }
    //Read NFC ID of a Client and store it. 
    public void FakeReadNFCID()
    {
        if (Tools.instance.netManager == null)
        {
            Tools.DisplayPopup(true, "Not connected to a server !");
            return;
        }
        Tools.instance.netManager.CmdNewClient(Random.Range(100000000, 999999999).ToString());
    }

    public void ReadNFCID(string recieved_Client)
    {
        
        if (Tools.instance.netManager == null)
        {
            Tools.DisplayPopup(true, "Got new client : " + recieved_Client + "\nBut not connected");
            return;
        }
        string clientID = recieved_Client.Remove(0, 1).Replace("\r", "");

        Debug.Log(clientID);
        Tools.instance.netManager.CmdNewClient(clientID);
        //_server.GetClientByID(clientID);
    }

    public void UpdateMyClient(string nfc_id, int nbRemainingBet, float gains)
    {
        if (_client == null)
            _client = new ClientLaura(nfc_id, nbRemainingBet, myClient);
        _client.nfc_id = nfc_id;
        _client.nbRemainingBet = nbRemainingBet;
        _client.gains = gains;
        DisplayMyClient();
    }

    public void DisplayMyClient()
    {
        myClient.UpdateClient(_client.nfc_id, _client.nbRemainingBet, _client.gains);
        myClient.Display();
    }

    public void AddBet(int number)
    {
        if (_client == null)
        {
            Tools.DisplayPopup(true, "Please select a client");
        }else
        {
            Tools.instance.netManager.CmdAddBet(_client.nfc_id, number);
            Tools.DisplayPopup(true, "RequestSent");        
        }
    }

}


