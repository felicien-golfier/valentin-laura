using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class server : MonoBehaviour
{

    private List<store> stores;
    private List<Client> clients;

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
            clients = new List<Client>();
        //DisplayAllClients();
    }

    private Client FindClient(string nfc_id)
    {
        foreach (Client c in clients)
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
        Client _client = FindClient(nfc_id);
        if (_client != null)
        {
            _client.nbRemainingBet += addBetNumber;
            _client.DisplayClient(true);
            return true;
        }
        Debug.LogError("Client doesn't exist");
        return false;
    }

    public Client NewClient(string nfc_id)
    {
        Client _client = GetClient(nfc_id);
        if (_client == null)
        {
            _client = new Client(nfc_id, 0, GetNextEmptyClientGo());
            clients.Add(_client);
            _client.DisplayClient(true);
        }

        return _client;
    }

    private ClientToDiplay GetNextEmptyClientGo()
    {
        try
        {
            return clientsInterface[clients.Count];
        }catch
        {
            tools.DisplayPopup(true, "It's a test version Only " +clientsInterface.Count +" clients possible.");
        }
        return null;
    }

    public Client GetClient(string id)
    {
        return clients.Find(x => x.nfc_id == id);
    }

    public float Tir(string id)
    {
        Client _client = FindClient(id);
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
            tools.DisplayPopupFromThread("You Won " + gain + "!!");
            return gain;
        }
    }

    public float GetGain()
    {
        System.Random rnd = new System.Random();
        return (float) Math.Floor( Math.Exp(rnd.Next(lower,greater)));
    }
}
