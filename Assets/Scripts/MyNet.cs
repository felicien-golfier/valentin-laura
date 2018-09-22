using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNet : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public override void OnStartServer()
    {
            tools.instance.ImServer = true;
    }
}