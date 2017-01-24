using UnityEngine;
using System.Collections;

public class ClicToMove : MonoBehaviour 
	{
		Vector3 newPosition;
	public float speed = 5f;
	public float rotSpeed = 10f;
	public GameObject targetPos;
	Vector3 dist;
	Quaternion newRot;

		void Start () {
			newPosition = transform.position;
		}
		void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				targetPos.GetComponent<LineRenderer> ().enabled = true;
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider.gameObject.layer == 8){
				newPosition = new Vector3 (hit.point.x, 0f, hit.point.z);
				dist = newPosition - transform.position;
				newRot = Quaternion.LookRotation (dist);
				int distancetotale = (int)Vector3.Distance (transform.position, hit.point);
				Debug.Log ("distance: " + distancetotale);
				targetPos.transform.position = hit.point;
				targetPos.GetComponent<DrawLineToPlayer> ().SetStartingLinePoint ();
					GetComponent<PlayerShipStats> ().isMoving = true;
					}}
			}
		transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, newRot, rotSpeed * Time.deltaTime);

		}
	}