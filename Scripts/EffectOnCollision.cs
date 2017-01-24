using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnCollision : MonoBehaviour {

	public GameObject partEffect;
	private float ticTimer;
	private float consRate = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		foreach(ContactPoint contact in other.contacts)
		{
			Instantiate (partEffect, contact.point, Quaternion.identity);
		}
	}
	void OnCollisionStay (Collision other)
	{
		if (Time.time > ticTimer) {
			ticTimer = Time.time + consRate;
			if (other.gameObject.tag == "Player") {
				other.gameObject.GetComponent<PlayerShipStats> ().shipIntegrity -= 1;
			}
		}
	}
}
