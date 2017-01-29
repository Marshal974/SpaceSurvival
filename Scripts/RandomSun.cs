using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSun : MonoBehaviour {

	public Light sun;
	public Flare[] flares;
	// Use this for initialization
	void Start () {
		sun = GetComponent<Light> ();
		sun.color = Random.ColorHSV ();
		sun.intensity = Random.Range (0.2f, 5f);
		sun.flare = flares[Random.Range(0, flares.Length)];
		sun.transform.rotation = Quaternion.Euler (Random.Range(0f,360f),Random.Range(0f,360f),0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
