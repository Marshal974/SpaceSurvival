using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamScript : MonoBehaviour {

	public GameObject targetObj;
	public float y;


	// Use this for initialization
	void Start () {
		y = transform.position.y;
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (targetObj.transform.position.x, y, targetObj.transform.position.z);
		
	}
}
