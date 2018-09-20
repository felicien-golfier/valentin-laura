using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Client
{
    public int nfc_id = 0;
    public int nbRemainingBet = 0;
    public ClientToDiplay go;

    public Client(int nfc_id, int nbRemainingBet, ClientToDiplay go)
    {
        this.nfc_id = nfc_id;
        this.nbRemainingBet = nbRemainingBet;
        this.go = go;
    }

    public void DisplayClient(bool display)
    {
        if (!go.client)
        {
            tools.instance.DisplayPopup(true, "This client has no interface.");
        }
        go.UpdateClient(nfc_id, nbRemainingBet);
        go.Display(display);
    }
}
