using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class server : MonoBehaviour
{

    public List<store> stores;
    public List<client> clients;

    private client FindClient(List<client> clients, int nfc_id)
    {
        foreach (client c in clients)
        {
            if (c.nfc_id == nfc_id) return c;
        }
        return null;
    }

    private void Start()
    {
        if (clients == null)
            clients = new List<client>();
    }
}
