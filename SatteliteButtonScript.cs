using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatteliteButtonScript : MonoBehaviour {

	public GameObject playerInt;
	// Use this for initialization
	void Start () {
		playerInt = GameObject.Find ("PlayerInterface");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ClicOnBtn()
	{
		if (playerInt.GetComponent<RadarInterface> ().isCrewed) 
		{
			playerInt.GetComponent<PlayerInterfaceManager> ().PlayClicAcceptedSound ();
			playerInt.GetComponent<RadarInterface> ().Scanning ();
			Destroy (gameObject);
			return;
		}
		playerInt.GetComponent<PlayerInterfaceManager> ().PlayClicDeniedSound ();
	}
}
