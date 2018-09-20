using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ClientToDiplay {

    public GameObject client;

    private Text id;
    private Text bet;

    public void Display(bool active = true)
    {
        client.SetActive(active);
    }

    public void UpdateClient(int id, int bet)
    {
        if (this.id == null || this.bet == null)
        {
            init();
        }
        this.id.text = id.ToString();
        this.bet.text = bet.ToString();
    }

    private void init()
    {
        this.id = client.transform.Find("id").GetComponent<Text>();
        this.bet = client.transform.Find("Nu").GetComponent<Text>();
    }
}
