using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class ClientManager : MonoBehaviour {

	public bool localServer;
	public Button connectButton;
	HostData gameData;

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
		if (localServer) {
			if (onWindows) Network.Connect ("192.168.0.11", 8001);
			else if (onMac) Network.Connect ("192.168.0.15", 8001);
		}
	}
	
	void Update() {
		//Debug.Log(Time.realtimeSinceStartup);
		if (MasterServer.PollHostList().Length != 0) {
			HostData[] hostData = MasterServer.PollHostList();
			int i = 0;
			while (i < hostData.Length) { // there should only be 1 game
				Debug.Log("Game name: " + hostData[i].gameName);
				Text buttonText = connectButton.GetComponentInChildren<Text>();
				buttonText.text = "Connect to " + hostData[i].gameName;
				gameData = hostData[i];
				break;
			}
			MasterServer.ClearHostList();
		}
	}

	public void connectWithNAT(){
		Network.Connect (gameData);
	}

	void OnConnectedToServer(){
		connectButton.gameObject.SetActive(false);
	}
}
