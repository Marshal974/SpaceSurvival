using UnityEngine;
using System.Collections;

namespace VTL.RTSCamera
{
	public class RTSCameraControl : MonoBehaviour
	{
		// Limits
		float startFOV = 60;
		float minFOV = 15f;
		float maxFOV = 90f;
		float maxDistance = 900f; // from 0,0,0

		// WASDQE Panning
		public float minPanSpeed = 0.1f;    // Starting panning speed
		public float maxPanSpeed = 1000f;   // Max panning speed
		public float panTimeConstant = 20f; // Time to reach max panning speed

		// Mouse right-down rotation
		public float rotateSpeed = 10; // mouse down rotation speed about x and y axes
		public float zoomSpeed = 2;    // zoom speed

		float panT = 0;
		float panSpeed = 10;
		Vector3 panTranslation;
		bool wKeyDown = false;
		bool aKeyDown = false;
		bool sKeyDown = false;
		bool dKeyDown = false;
		bool qKeyDown = false;
		bool eKeyDown = false;

		Vector3 lastMousePosition;
		new Camera camera;

		void Start()
		{
			camera = GetComponentInChildren<Camera>();
		}

		void Update()
		{
			//
			// WASDQE Panning

			// read key inputs
			wKeyDown = Input.GetKey(KeyCode.Z);
			aKeyDown = Input.GetKey(KeyCode.Q);
			sKeyDown = Input.GetKey(KeyCode.S);
			dKeyDown = Input.GetKey(KeyCode.D);
			qKeyDown = Input.GetKey(KeyCode.A);
			eKeyDown = Input.GetKey(KeyCode.E);

			// determine panTranslation
			panTranslation = Vector3.zero;
			if (dKeyDown && !aKeyDown)
				panTranslation += Vector3.right * Time.deltaTime * panSpeed;
			else if (aKeyDown && !dKeyDown)
				panTranslation += Vector3.left * Time.deltaTime * panSpeed;

			if (wKeyDown && !sKeyDown)
				panTranslation += Vector3.forward * Time.deltaTime * panSpeed;
			else if (sKeyDown && !wKeyDown)
				panTranslation += Vector3.back * Time.deltaTime * panSpeed;

			if (qKeyDown && !eKeyDown)
				panTranslation += Vector3.down * Time.deltaTime * panSpeed;
			else if (eKeyDown && !qKeyDown)
				panTranslation += Vector3.up * Time.deltaTime * panSpeed;
			transform.Translate(panTranslation, Space.Self);

			// Update panSpeed
			if (wKeyDown || aKeyDown || sKeyDown ||
				dKeyDown || qKeyDown || eKeyDown)
			{
				panT += Time.deltaTime / panTimeConstant;
				panSpeed = Mathf.Lerp(minPanSpeed, maxPanSpeed, panT * panT);
			}
			else
			{
				panT = 0;
				panSpeed = minPanSpeed;
			}

			//
			// Mouse Rotation
			if (Input.GetMouseButton(2))
			{
				// if the game window is separate from the editor window and the editor
				// window is active then you go to right-click on the game window the
				// rotation jumps if  we don't ignore the mouseDelta for that frame.
				Vector3 mouseDelta;
				if (lastMousePosition.x >= 0 &&
					lastMousePosition.y >= 0 &&
					lastMousePosition.x <= Screen.width &&
					lastMousePosition.y <= Screen.height)
					mouseDelta = Input.mousePosition - lastMousePosition;
				else
					mouseDelta = Vector3.zero;

				var rotation = Vector3.up * Time.deltaTime * rotateSpeed * mouseDelta.x;
				rotation += Vector3.left * Time.deltaTime * rotateSpeed * mouseDelta.y;
				transform.Rotate(rotation, Space.Self);

				// Make sure z rotation stays locked
				rotation = transform.rotation.eulerAngles;
				rotation.z = 0;
				transform.rotation = Quaternion.Euler(rotation);
			}

			lastMousePosition = Input.mousePosition;

			//
			// Mouse Zoom
//			camera.fieldOfView -= Input.mouseScrollDelta.y * zoomSpeed;
			camera.fieldOfView = Mathf.Clamp(camera.fieldOfView - Input.mouseScrollDelta.y * zoomSpeed, minFOV, maxFOV);
		}
	}
}
//{
//		public float ScrollSpeed = 15f;
//	public float ScrollEdge = 0.01f;
//
//	private int HorizontalScroll = 1;
//	private int VerticalScroll  = 1;
//	private int DiagonalScroll = 1;
//
//	public float PanSpeed = 10f;
//
//	Vector2 ZoomRange = new Vector2(-5,5);
//	float CurrentZoom = 0f;
//	float ZoomZpeed = 1f;
//	float ZoomRotation = 1f;
//
//	private Vector3 InitPos;
//	private Vector3 InitRotation;
//
//
//
//	void Start()
//	{
//		//Instantiate(Arrow, Vector3.zero, Quaternion.identity);
//
//		InitPos = transform.position;
//		InitRotation = transform.eulerAngles;
//
//	}
//
//	void Update ()
//	{
//		float yPos;
//		float xRot;
//		//PAN
//		if ( Input.GetKey("mouse 2") )
//		{
//			//(Input.mousePosition.x - Screen.width * 0.5)/(Screen.width * 0.5)
//
//			transform.Translate( transform.right* Time.deltaTime * PanSpeed * (Input.mousePosition.x - Screen.width * 0.5f)/(Screen.width * 0.5f), Space.World);
//			transform.Translate(transform.forward * Time.deltaTime * PanSpeed * (Input.mousePosition.y - Screen.height * 0.5f)/(Screen.height * 0.5f), Space.World);
//
//		}
//		else
//		{
//			if ( Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge) )
//			{
//				transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
//			}
//			else if ( Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= Screen.width * ScrollEdge )
//			{
//				transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.World);
//			}
//
//			if ( Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge) )
//			{
//				transform.Translate(Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
//			}
//			else if ( Input.GetKey(KeyCode.S) || Input.mousePosition.y <= Screen.height * ScrollEdge )
//			{
//				transform.Translate(Vector3.forward * Time.deltaTime * -ScrollSpeed, Space.World);
//			}
//		}
//
//		//ZOOM IN/OUT
//
//		CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 10 * ZoomZpeed;
//
//		CurrentZoom = Mathf.Clamp(CurrentZoom,ZoomRange.x,ZoomRange.y);
//		yPos = transform.position.y - (InitPos.y + CurrentZoom) * 0.1f;
//		yPos = transform.position.y - yPos;
//		transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
//		xRot = (transform.eulerAngles.x - (InitRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
//		transform.rotation = new Quaternion (xRot, transform.rotation.y, transform.rotation.z, Time.deltaTime*20f);
//
//	}
//}
