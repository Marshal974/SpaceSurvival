using UnityEngine;
using System.Collections;

public class ClicToMove : MonoBehaviour 
	{
		Vector3 newPosition;
	public LayerMask layerToTarget;
	public float speed = 5f;
	public float rotSpeed = 10f;
	public GameObject targetPos;
	public Camera camPlayer;
	Vector3 dist;
	Vector3 newPos;
	Quaternion newRot;
	Quaternion  _newRot;

		void Start () {
			newPosition = transform.position;
		}
		void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				targetPos.GetComponent<LineRenderer> ().enabled = true;
				RaycastHit hit;
			Ray ray = camPlayer.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 15000f, layerToTarget))
				{
				if (hit.collider.gameObject.layer == 8) {
					newPosition = new Vector3 (hit.point.x, 0f, hit.point.z);
					dist = newPosition - transform.position;
					_newRot = Quaternion.LookRotation (dist);
					int distancetotale = (int)Vector3.Distance (transform.position, hit.point);
					Debug.Log ("distance: " + distancetotale);
					if (distancetotale > 80) {
						targetPos.transform.position = hit.point;
						targetPos.GetComponent<DrawLineToPlayer> ().SetStartingLinePoint ();
						GetComponent<PlayerShipStats> ().StartToMove ();
						newPos = newPosition;
						newRot = _newRot;
					}
				}
			}
			}
		transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, newRot, rotSpeed * Time.deltaTime);

		}
	}