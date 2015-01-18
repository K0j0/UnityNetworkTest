using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ClientManager : MonoBehaviour {

	public bool localServer;
	
	void Awake() {
		Debug.Log ("My os is " + SystemInfo.operatingSystem);
		Debug.Log("My ip address is " + Network.player.ipAddress);
		
		MasterServer.ClearHostList();
		MasterServer.RequestHostList("KojoProto");
	}
	
	// Use this for initialization
	void Start () {
		bool onWindows = Regex.IsMatch(SystemInfo.operatingSystem, "Windows");
		bool onMac = Regex.IsMatch(SystemInfo.operatingSystem, "Mac OS X");
		Debug.Log ("On windows? " + onWindows + " On a Mac? " + onMac);
		
		// connect to my mac
		if(onWindows){
			if(localServer) Network.Connect("192.168.0.11", 8001);
			else connectToNAT();
		}
		// connect to my PC
		else if(onMac){
			if(localServer) Network.Connect("192.168.0.15", 8001);
			else connectToNAT();
		}
	}
	
	void connectToNAT(){
	
	}
	
	void Update() {
		Debug.Log(Time.realtimeSinceStartup);
		if (MasterServer.PollHostList().Length != 0) {
			HostData[] hostData = MasterServer.PollHostList();
			int i = 0;
			while (i < hostData.Length) {
				Debug.Log("Game name: " + hostData[i].gameName);
				i++;
			}
			MasterServer.ClearHostList();
		}
	}
}
