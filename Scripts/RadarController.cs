using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {

	public bool radarActive;
	public Transform radarArea;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 11) 
		{
			other.gameObject.layer = 10;
		}
		
	}

	public void ActivateTheRadar()
	{
		radarActive = true;
		radarArea.localScale = new Vector3 (6000,1500,3000);
	}

	public void DesactivateTheRadar()
	{
		radarActive = false;
		radarArea.localScale = new Vector3 (600,200,600);
	}
		
}
