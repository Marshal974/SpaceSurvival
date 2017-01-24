using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {
	public GameObject eventManager;
	public int timeToAdd;
	public bool added;

	// Use this for initialization
	void Start () {
		eventManager = GameObject.FindGameObjectWithTag ("Manager");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy == true && added == false) {
			eventManager.GetComponent<SceneEvent> ().actualTime =+ timeToAdd;
			added = true;
					}
		if (gameObject.activeInHierarchy == false && added == true) {
			eventManager.GetComponent<SceneEvent> ().actualTime =+ -timeToAdd;
			added = false;
			Debug.Log ("removed");
		}
	
	}
}
