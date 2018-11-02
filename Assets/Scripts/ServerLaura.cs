using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerLaura : MonoBehaviour
{
    private List<ClientLaura> clients;

    public List<ClientToDiplay> clientsInterface;

    public int lower = -99;
    public int greater = 13;

    private void Update()
    {
        DisplayAllClients();
    }

    private void Start()
    {
        if (clients == null)
            clients = new List<ClientLaura>();
        DisplayAllClients();
    }


    private ClientLaura FindClient(string nfc_id)
    {
        foreach (ClientLaura c in clients)
        {
            if (c.nfc_id == nfc_id) return c;
        }
        return null;
    }
    public void DisplayAllClients(bool display=true)
    {
        clients.ForEach(c => { c.DisplayClient(display);});
    }

    public bool UpdateClient(string nfc_id, int addBetNumber)
    {
        ClientLaura _client = FindClient(nfc_id);
        if (_client != null)
        {
            _client.nbRemainingBet += addBetNumber;
            _client.DisplayClient(true);
            return true;
        }
        Debug.LogError("Client doesn't exist");
        return false;
    }

    private ClientToDiplay GetNextEmptyClientGo()
    {
        try
        {
            return clientsInterface[clients.Count];
        }catch
        {
            Tools.DisplayPopup(true, "It's a test version Only " +clientsInterface.Count +" clients possible.");
        }
        return null;
    }

    public void NewClient(string nfc_id)
    {
        ClientLaura _client = GetClientByID(nfc_id);
        Tools.DisplayPopup(true, "NewClient");
        if (_client == null)
        {
            _client = new ClientLaura(nfc_id, 0, GetNextEmptyClientGo());
            clients.Add(_client);
            _client.DisplayClient(true);
        }
            Tools.instance.netManager.RpcSendClient(_client.nfc_id, _client.nbRemainingBet, _client.gains);
    }

    public void AddBet(string nfc_id, int number)
    {
        ClientLaura _client = GetClientByID(nfc_id);
        Tools.DisplayPopup(true, "Add Bet !");

        if (_client == null)
        {
            _client = new ClientLaura(nfc_id, number, GetNextEmptyClientGo());
            clients.Add(_client);
            _client.DisplayClient(true);
        }
        else
        {
            _client.nbRemainingBet += number;
        }

        Tools.instance.netManager.RpcSendClient(_client.nfc_id, _client.nbRemainingBet, _client.gains);
    }

    public void GetClientToStore(string id)
    {
        ClientLaura tmpClient = GetClientByID(id);
        Tools.instance.netManager.RpcSendClient(tmpClient.nfc_id, tmpClient.nbRemainingBet, tmpClient.gains);
    }

    public ClientLaura GetClientByID(string id)
    {
         return clients.Find(x => x.nfc_id == id);
    }

    public float Tir(string id)
    {
        ClientLaura _client = FindClient(id);
        if (_client == null)
        {
            Debug.Log("Client Not registered !!");
            return -1;
        }else if (_client.nbRemainingBet <= 0)
        {
            Debug.Log("Client Has No remaining Bet !!");
            return -1;
        }
        else
        {
            float gain = GetGain();
            if (gain <= 0)
                return 0;
            _client.Won(gain);
            Tools.DisplayPopupFromThread("You Won " + gain + "!!");
            return gain;
        }
    }

    public float GetGain()
    {
        System.Random rnd = new System.Random();
        return (float) Math.Floor( Math.Exp(rnd.Next(lower,greater)));
    }


}
