using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class server : MonoBehaviour
{

    public List<store> stores;
    public List<Client> clients;
    public List<ClientToDiplay> clientsInterface;

    private void Update()
    {
        
    }

    private void Start()
    {
        if (clients == null)
            clients = new List<Client>();
        //DisplayAllClients();
    }

    private Client FindClient(List<Client> clients, int nfc_id)
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

    public bool UpdateClient(int nfc_id, int addBetNumber)
    {
        Client _client = FindClient(clients, nfc_id);
        if (_client != null)
        {
            _client.nbRemainingBet += addBetNumber;
            _client.DisplayClient(true);
            return true;
        }
        Debug.LogError("Client doesn't exist");
        return false;
    }

    public Client NewClient(int nfc_id, int addBetNumber = 0)
    {
        Client _client = new Client(nfc_id, addBetNumber, GetNextEmptyClientGo());
        clients.Add(_client);
        _client.DisplayClient(true);
        return clients[clients.Count - 1];
    }

    private ClientToDiplay GetNextEmptyClientGo()
    {
        try
        {
            return clientsInterface[clients.Count];
        }catch(Exception e)
        {
            tools.instance.DisplayPopup(true, "It's a test version Only " +clientsInterface.Count +" clients possible.");
        }
        return null;
    }

    public Client GetClient(int id)
    {
        return clients.Find(x => x.nfc_id == id);
    }
}
