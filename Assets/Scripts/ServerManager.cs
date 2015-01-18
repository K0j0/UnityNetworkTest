using UnityEngine;
using System.Collections;

public class ServerManager : MonoBehaviour {
	bool useNat;
	
	// Use this for initialization
	void Start () {		
		useNat = !Network.HavePublicAddress();
		Debug.Log("Has public ip? " + Network.HavePublicAddress() + " Use NAT? " + useNat);
		Network.InitializeServer(8, 8001, useNat);
		MasterServer.RegisterHost("KojoTest", "game1");
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
		Debug.Log("My external ip address is " + Network.player.externalIP);
	}
}
