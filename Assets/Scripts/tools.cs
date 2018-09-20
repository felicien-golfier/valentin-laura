using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tools : MonoBehaviour {

    public GameObject popup;
    public Text popupText;
    public GameObject server;
    public GameObject store;

    private static tools _instance;
    public static tools instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }


    public void DisplayPopup(bool display, string txt = "")
    {
        if (display) popupText.text = txt;
        popup.SetActive(display);
    }

    public void DisplayThisShit(GameObject go)
    {
        go.SetActive(true);
    }
    public void StopDisplayThisShit(GameObject go)
    {
        go.SetActive(false);
    }

    public void ChangeServerClient()
    {
        if (server.active)
        {
            server.SetActive(false);
            store.SetActive(true);
        }else
        {
            server.SetActive(true);
            store.SetActive(false);
        }
    }

}

