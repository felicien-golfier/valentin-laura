using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store : MonoBehaviour {


    public server _server;
    public ClientToDiplay myClient;

    private int laura_id;
    private Client _client;


    //Read NFC ID of a Client and store it. 
    public void ReadNFCID()
    {
        _client = _server.NewClient(Random.Range(100000000, 999999999), 0);
        UpdateMyClient();
    }

    private void Activate()
    {

    }

    public void UpdateMyClient()
    {
        _client = _server.GetClient(_client.nfc_id);
        myClient.UpdateClient(_client.nfc_id, _client.nbRemainingBet);
        myClient.Display();
    }

    public void AddBet(int number)
    {
        if (_client == null)
        {
            tools.instance.DisplayPopup(true, "Please select a client");
        }else
        {
            if (_server.UpdateClient(_client.nfc_id, number))
                tools.instance.DisplayPopup(true, "RequestSent");        
        }
    }
}


