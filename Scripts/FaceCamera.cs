using UnityEngine;
using System.Collections;


public class FaceCamera : MonoBehaviour {
	public float rotateyAdd = 180f;
	public float rotatezAdd =0;
	public float rotatexAdd =0;



	void FixedUpdate () {

			this.transform.LookAt (Camera.main.transform.position);
		this.transform.Rotate (new Vector3 (rotatexAdd, rotateyAdd, rotatezAdd));

	}
}
