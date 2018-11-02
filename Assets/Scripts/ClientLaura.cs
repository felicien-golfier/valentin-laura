using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ClientLaura
{
    public string nfc_id = null;
    public int nbRemainingBet = 0;
    public ClientToDiplay go;
    public float gains = 0;

    public ClientLaura(string nfc_id, int nbRemainingBet, ClientToDiplay go)
    {
        this.nfc_id = nfc_id;
        this.nbRemainingBet = nbRemainingBet;
        this.go = go;
    }

    public void DisplayClient(bool display)
    {
        if (!go.client)
        {
            Tools.DisplayPopup(true, "This client has no interface.");
        }
        go.UpdateClient(nfc_id, nbRemainingBet, gains);
        go.Display(display);

    }

    public bool Won(float gain)
    {
        if (nbRemainingBet > 0)
        {
            nbRemainingBet--;
            gains += gain;
            return true;
        }
            return false;        
    }
}
