using UnityEngine;
using System.Collections;

public class DrawLineToPlayer : MonoBehaviour {

	LineRenderer lineR;
	GameObject player;

	// Use this for initialization
	void Start () {
		lineR = gameObject.GetComponent<LineRenderer> ();
		player = GameObject.Find ("PlayerObj");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		lineR.SetPosition (1, player.transform.position);
	
	}
	public void SetStartingLinePoint()
	{
		lineR.SetPosition (0, transform.position);
	}

}
