using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ClientToDiplay {

    public GameObject client;

    private Text id;
    private Text bet;
    private Text gains;

    public void Display(bool active = true)
    {
        client.SetActive(active);
    }

    public void UpdateClient(string id, int bet, float gains)
    {
        if (this.id == null || this.bet == null || this.gains == null)
        {
            init();
        }
        this.id.text = id.ToString();
        this.gains.text = gains.ToString();
        this.bet.text = bet.ToString();
    }

    private void init()
    {
        this.id = client.transform.Find("id").GetComponent<Text>();
        this.bet = client.transform.Find("Nu").GetComponent<Text>();
        this.gains = client.transform.Find("gains").GetComponent<Text>();
    }
}
