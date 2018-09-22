using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store : MonoBehaviour {


    public server _server;
    public ClientToDiplay myClient;

    private int laura_id;
    private Client _client;
    private char separator = ':';
    private string lastUdpPacket;

    private void Update()
    {
        if (_client != null)
            UpdateMyClient();
        string  udp = tools.instance.udp.getLatestUDPPacket();
        if (udp != "" && udp != lastUdpPacket)
        {
            lastUdpPacket = udp;
            ReadNFCID(lastUdpPacket);
        }
    }
    //Read NFC ID of a Client and store it. 
    public void FakeReadNFCID()
    {
        _client = _server.NewClient(Random.Range(100000000, 999999999).ToString());
        UpdateMyClient();
    }
    public void ReadNFCID(string recieved_Client)
    {
        string clientID = recieved_Client.Split(separator)[1].Remove(0, 1).Replace("\r","");

        Debug.Log(clientID);
        _client = _server.NewClient(clientID);
        UpdateMyClient();
    }

    public void UpdateMyClient()
    {
        _client = _server.GetClient(_client.nfc_id);
        myClient.UpdateClient(_client.nfc_id, _client.nbRemainingBet, _client.gains);
        myClient.Display();
    }

    public void AddBet(int number)
    {
        if (_client == null)
        {
            tools.DisplayPopup(true, "Please select a client");
        }else
        {
            if (_server.UpdateClient(_client.nfc_id, number))
                tools.DisplayPopup(true, "RequestSent");        
        }
    }
}


