using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayClients : MonoBehaviour {

    public clientToDisplay[] clientsToDisplay;
	// Use this for initialization
	void Start () {
		
	}
	
    public void DisplayClient(client givenClient)
    {

    }

    [System.Serializable]
    public class clientToDisplay
    {
        client _client;
        GameObject display;
    }

}
