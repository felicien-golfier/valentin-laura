﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerLaura : MonoBehaviour
{
    private List<ClientLaura> clients;

    public List<ClientToDiplay> clientsInterface;
    public Text logs;

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


    //private ClientLaura FindClient(string nfc_id)
    //{
    //    foreach (ClientLaura c in clients)
    //    {
    //        if (c.nfc_id == nfc_id) return c;
    //    }
    //    return null;
    //}
    public void DisplayAllClients(bool display=true)
    {
        clients.ForEach(c => { c.DisplayClient(display);});
    }

    //public bool UpdateClient(string nfc_id, int addBetNumber)
    //{
    //    ClientLaura _client = FindClient(nfc_id);
    //    if (_client != null)
    //    {
    //        _client.nbRemainingBet += addBetNumber;
    //        _client.DisplayClient(true);
    //        log("Client " + nfc_id + " updated with " + addBetNumber + " more bets. Remaining bets now " + _client.nbRemainingBet);
    //        return true;
    //    }
    //    log("Client " + nfc_id + " doesn't exist !");
    //    Debug.LogError("Client doesn't exist");
    //    return false;
    //}

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
        log("New client " + nfc_id);
        if (_client == null)
        {
            _client = new ClientLaura(nfc_id, 0, GetNextEmptyClientGo());
            clients.Add(_client);
            _client.DisplayClient(true);
        }
            Tools.instance.netManager.RpcSendClient(_client.nfc_id, _client.nbRemainingBet, _client.gains);
    }

    public void AddBet(string nfc_id, int addBetNumber)
    {
        ClientLaura _client = GetClientByID(nfc_id);
        
        if (_client == null)
        {
            _client = new ClientLaura(nfc_id, addBetNumber, GetNextEmptyClientGo());
            clients.Add(_client);
            _client.DisplayClient(true);
            log("New client " + nfc_id + " with bet " + addBetNumber + " more bets. Remaining bets now " + _client.nbRemainingBet);
        }
        else
        {
            _client.nbRemainingBet += addBetNumber;
            log("Client " + nfc_id + " Added bet " + addBetNumber + " more bets. Remaining bets now " + _client.nbRemainingBet);
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
        ClientLaura _client = GetClientByID(id);
        if (_client == null)
        {
            log("Client "+id+" shot but is not registered");
            return -1;
        }else if (_client.nbRemainingBet <= 0)
        {
            log("Client " + id + " shot but has no remaining bet");
            return -1;
        }
        else
        {
            float gain = GetGain();
            if (gain <= 0)
                return 0;
            _client.Won(gain);
            log("Client " + id + " shot and won "+gain+" gains");
            return gain;
        }
    }

    public float GetGain()
    {
        System.Random rnd = new System.Random();
        return (float) Math.Floor( Math.Exp(rnd.Next(lower,greater)));
    }

    private void log(string newline)
    {
        logs.text = newline + "\n" + logs.text;
    }
}
