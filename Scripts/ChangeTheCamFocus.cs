using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTheCamFocus : MonoBehaviour {

	public LayerMask layerToTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonUp(0))

			{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 15000f, layerToTarget))
			{
				if (hit.collider.gameObject.GetComponent<OnClicProcede> () != null) 
				{
					GetComponent<MyCamera> ().FocusOnInterestPoint (hit.collider.gameObject);
					hit.collider.gameObject.GetComponent<OnClicProcede> ().OnClicEvent ();
				}
			}
			}
		
	}
}
