using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.layer == 11) {
			other.gameObject.layer = 10;
		}
	}
}
