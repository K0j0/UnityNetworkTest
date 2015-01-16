using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class CubeScript : MonoBehaviour {
	public bool isServer;
	public bool localServer;
	bool useNat;


	// Use this for initialization
	void Start () {
		Debug.Log ("My os is " + SystemInfo.operatingSystem);
		bool onWindows = Regex.IsMatch(SystemInfo.operatingSystem, "Windows");
		bool onMac = Regex.IsMatch(SystemInfo.operatingSystem, "Mac OS X");
		Debug.Log ("On windows? " + onWindows + " On a Mac? " + onMac);
		Debug.Log("My ip address is " + Network.player.ipAddress);
		
		
		if(isServer){
			useNat = !Network.HavePublicAddress();
			Debug.Log("Has public ip? " + Network.HavePublicAddress() + " Use NAT? " + useNat);
			Network.InitializeServer(8, 8001, useNat);			
			
		
		} else{
			// connect to my mac
			if(onWindows){
				if(localServer) Network.Connect("192.168.0.11", 8001);
				else;
			}
			// connect to my PC
			else if(onMac){
				if(localServer) Network.Connect("192.168.0.15", 8001);
				else;
			}
			
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
		Debug.Log("My external ip address is " + Network.player.externalIP);
	}
}
