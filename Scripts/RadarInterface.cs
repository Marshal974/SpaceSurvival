	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class RadarInterface : MonoBehaviour {

		public bool radarActive;
		public Camera radarCam;
		public Transform radarArea;
		public int moduleLvl = 5;
		public int moduleConsumption = 1;
		public bool actifTab;
		public GameObject RadarMessPrefab;
		public GameObject childMenu;
		public Text crewName;
		public Text crewDescriptif;
		private CrewCharacter crewCharScript;
		public Button assignCrew;
		public Button crewLeave;
		public GameObject CrewPanel;
	public Text radarRangeTxt;

	public float timeBetweenTics = 2;
		public bool isCrewed;
		
		private float nextTic;
		private bool alreadyRunning;

	void Start()
	{
		nextTic = Time.time;
	}
	void Update()
	{			
		if (Time.time > nextTic) 
		{
		while (radarActive) 
		{

				StartCoroutine (ScanWithRadar ());
				nextTic = Time.time + timeBetweenTics;
				return;
			}
		}
	}

		public void ActivateTheRadar()
		{
			radarActive = true;
			radarArea.localScale = new Vector3 (6000,1500,3000);
		}

	IEnumerator ScanWithRadar()
	{
		radarCam.enabled = true;
		yield return new WaitForEndOfFrame();
		radarCam.enabled = false;
	}

		public void DesactivateTheRadar()
		{
			radarActive = false;
			radarArea.localScale = new Vector3 (600,200,600);
		}

		public void ToggleMenu(){
			GetComponent<PlayerInterfaceManager>().ChangeCamPosition ();
			if (actifTab == false){
				GetComponent<PlayerInterfaceManager>().CloseAllOtherModules ();
			GetComponent<PlayerInterfaceManager>().radarRoomPanel =! GetComponent<PlayerInterfaceManager>().radarRoomPanel;

				childMenu.SetActive (true);
				actifTab = true;
				if (isCrewed) 
				{


				}

				return;
			}
			if (actifTab == true){
			GetComponent<PlayerInterfaceManager>().radarRoomPanel =! GetComponent<PlayerInterfaceManager>().radarRoomPanel;

				childMenu.SetActive (false);
				actifTab = false;	

				if (GetComponent<PlayerInterfaceManager> ().crewSelectPanel == true) 
				{
					GetComponent<CrewSelectInterface> ().ToggleMenu ();
				}



			}

		}	
		//make the module Crewed
		public void GetAGuy(string name)
		{

			isCrewed = true;
			CrewPanel.SetActive (true);
			assignCrew.gameObject.SetActive (false);
			gameObject.GetComponent<CrewSelectInterface> ().CrewPanel.SetActive (false);
			crewCharScript = GameObject.Find(name).GetComponent<CrewCharacter> (); 
			crewName.text = crewCharScript.nom;

			gameObject.GetComponent<CrewSelectInterface> ().actifTab = false;
			if (alreadyRunning) 
			{
				return;
			}

			StartCoroutine (MakeAGuyJoinTheRoom());
		}
		public void LooseYourGuy()
		{
			isCrewed = false;
			DesactivateTheRadar ();

			MakeGuyAvailableOnTimer ();
		}

		public void MakeGuyAvailableOnTimer(){
			StartCoroutine(MakeAvailableOnT());
		}

		IEnumerator MakeAvailableOnT()
		{		
			crewDescriptif.text = crewCharScript.nom + " is packing.";
			yield return new WaitForSeconds (1f);
			crewDescriptif.text = crewCharScript.nom + " is packing...";
			yield return new WaitForSeconds (1f);
			crewDescriptif.text = crewCharScript.nom + " is getting ready to leave...";
			yield return new WaitForSeconds (1f);
			crewDescriptif.text = crewCharScript.nom + " is leaving the module.";
			yield return new WaitForSeconds (2f);
			crewCharScript.isAssigned = false;
			assignCrew.gameObject.SetActive (true);
			CrewPanel.SetActive (false);
			GameObject go = Instantiate (RadarMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
			go.GetComponentInChildren<Image> ().color = Color.green;
			go.GetComponentInChildren<Text> ().text = crewCharScript.nom + " is now available.";


		}

		IEnumerator MakeAGuyJoinTheRoom()
		{
			alreadyRunning = true;
			crewLeave.interactable = false;
			crewCharScript.isAssigned = true;

			crewDescriptif.text = crewCharScript.nom + " is on it's way !";
			GetComponent<PlayerInterfaceManager> ().crewSelectPanel = false;

			yield return new WaitForSeconds (3f);
			crewDescriptif.text = "No special skills.";
			crewLeave.interactable = true;

			if ((crewCharScript.navComp == true)) 
			{
				crewDescriptif.text = "Navigator level " + crewCharScript.lvlNavComp;
				ActivateTheRadar ();
				timeBetweenTics = Mathf.Round( 5/crewCharScript.lvlNavComp);
				radarRangeTxt.text = "Actualisation rate : "+ timeBetweenTics + "/s.";
			}
			alreadyRunning = false;
			GameObject go = Instantiate (RadarMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
			go.GetComponentInChildren<Image> ().color = Color.white;
			go.GetComponentInChildren<Text> ().text = crewCharScript.nom + " is in the radar room.";
		}

	}

