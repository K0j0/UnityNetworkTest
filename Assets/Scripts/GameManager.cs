using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject gallery;

	public void makeServerRequest(){
		networkView.RPC ("requestShowGallery", RPCMode.Server, true);
	}

	[RPC]
	public void showGallery(bool value){
		gallery.SetActive (value);
	}

}
