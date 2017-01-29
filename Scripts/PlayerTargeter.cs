using UnityEngine;
using System.Collections;

public class PlayerTargeter : MonoBehaviour {

	GameObject playerObj;

	// Use this for initialization
	void Start () {
	
		playerObj = GameObject.Find("PlayerObj");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter( Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			GetComponent<LineRenderer> ().enabled = false;
			other.GetComponent<PlayerShipStats> ().StopMoving ();

		}
	}
}
