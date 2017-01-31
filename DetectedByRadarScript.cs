using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedByRadarScript : MonoBehaviour {

	public GameObject messPrefab;
	public GameObject interestPointPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// ajouter ca dans la fenetre radar. en mode interactif.
	public void GetDetected()
	{
		GameObject go = Instantiate (interestPointPrefab);
	}

}
