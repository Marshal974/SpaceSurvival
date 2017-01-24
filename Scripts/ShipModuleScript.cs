using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipModuleScript : MonoBehaviour {
	public GameObject module;

	void Update(){

		if (module.activeInHierarchy) {
			this.gameObject.SetActive (true);
		} 

		else {
			this.gameObject.SetActive (false);
		}
}
}