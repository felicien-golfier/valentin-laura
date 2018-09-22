using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class tools : MonoBehaviour {

    private bool m_ImServer;
    public bool ImServer
    {
        set
        {
            m_ImServer = value;
            ChangeServerClient(!value);
        }
        get { return m_ImServer; }
    }
    public GameObject popup;
    public Text popupText;
    public GameObject server;
    public GameObject store;
    public UDPReceive udp;
    public MyNet myNet;
    private string PopupToDisplay = "";

    public NetworkManager networkManager;
    NetworkIdentity networkIdentity;

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
        ChangeServerClient(ImServer);
        
    }

    private void Update()
    {
        if (PopupToDisplay != "")
        {
            DisplayPopup(true, PopupToDisplay);
            PopupToDisplay = "";
        }        
    }

    public static void DisplayPopup(bool display, string txt = "")
    {
        if (display) instance.popupText.text = txt;
        instance.popup.SetActive(display);
    }

    public static void DisplayPopupFromThread(string txt)
    {
        instance.PopupToDisplay = txt;
    }

    public void DisplayThisShit(GameObject go)
    {
        go.SetActive(true);
    }
    public void StopDisplayThisShit(GameObject go)
    {
        go.SetActive(false);
    }

    public void PushChangeServerClient()
    {
        ChangeServerClient(null);
    }
    private void ChangeServerClient(bool? ServerToClient = null)
    {
        if (ServerToClient == false || (ServerToClient == null && server.activeSelf))
        {
            server.SetActive(false);
            store.SetActive(true);
        }
        else if(ServerToClient == true || (ServerToClient == null && !server.activeSelf))
        {
            server.SetActive(true);
            store.SetActive(false);
        }
    }

}

