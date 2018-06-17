using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class client
{
    public int nfc_id = 0;
    public int nbRemainingBet = 0;
    public GameObject go;

    public void DisplayClient(bool display)
    {
        go.SetActive(display);
        if (display)
        {
            go.transform.GetChild(0).GetComponent<Text>().text = nfc_id.ToString();
            go.transform.GetChild(1).GetComponent<Text>().text = nbRemainingBet.ToString();
        }
    }

}
