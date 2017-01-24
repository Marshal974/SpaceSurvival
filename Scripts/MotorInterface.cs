using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotorInterface : MonoBehaviour {

	public bool actifTab;
	public GameObject ChildMenu;
	public Slider ionicCons;
	public Slider tempRoom;
	public Text ionicSpeed;
	public Text ElecConsTxt;
	public Button engineSwitch;

	public Text coolingMsg;
	public Text coolingLvlMsg;
	public Button coolingBtn;
	public int coolerLvl = 1;

	private bool overheating;


	public GameObject CrewPanel;
	public Text crewName;
	public Text crewDescriptif;
	private CrewCharacter crewCharScript;
	public Button assignCrew;
	public Button crewLeave;
	public bool isCrewed;

	public void Start()
	{
		coolingMsg.text = "Cooling systems level " + coolerLvl + " are operational.";
	}

	public void Update()
	{
		if (overheating == false && tempRoom.value == tempRoom.maxValue) 
		{
			overheating = true;
			ionicCons.maxValue = 5;
			if (ionicCons.value > 5) {
				ionicCons.value = 5;
			}
		}
		if (overheating == true) 
		{
			if (tempRoom.value < tempRoom.maxValue * 80 / 100) 
			{
				overheating = false;
				ionicCons.maxValue = 30;

			}
		}
	}

	public void BreakTheModule()
	{
		coolerLvl = 0;
		coolingBtn.interactable = true;
		coolingBtn.gameObject.GetComponent<Image> ().color = Color.red;
	}

	public void FixTheModule()
	{
		
	}

	public void ToggleMenu(){
		GetComponent<PlayerInterfaceManager>().ChangeCamPosition ();
		GetComponent<PlayerInterfaceManager>().motorRoomPanel =! GetComponent<PlayerInterfaceManager>().motorRoomPanel;
		;
		if (actifTab == false){

			ChildMenu.SetActive (true);
			actifTab = true;
			if (isCrewed) 
			{
//				if ((gameObject.GetComponentInChildren <CrewCharacter> () as CrewCharacter) != null) {
					engineSwitch.interactable = true;
				if ((crewCharScript.techComp == true)) 
						{
						
						ionicCons.interactable = true;
					}

			}
				
			return;
		}
		if (actifTab == true){

			ChildMenu.SetActive (false);
			actifTab = false;	
			ionicCons.interactable = false;
			engineSwitch.interactable = false;
			if (GetComponent<PlayerInterfaceManager> ().crewSelectPanel == true) 
			{
				GetComponent<CrewSelectInterface> ().ToggleMenu ();
			}


						
		}

	}	

	public void SetMotorConsRate()
	{
		GameObject.Find ("PlayerObj").GetComponent<PlayerShipStats> ().motorPowerCons = Mathf.RoundToInt ( ionicCons.value);
		GameObject.Find ("PlayerObj").GetComponent<ClicToMove> ().speed = ionicCons.value/2;
		ionicSpeed.text = "Ionic speed: " + ionicCons.value / 2 + " m/s.";
		ElecConsTxt.text = ionicCons.value.ToString() + "/s.";
	}
	public void StopMotorModule()
	{
		ionicCons.value = 0;
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
	

		StartCoroutine (MakeAGuyJoinTheRoom());
	}
	public void LooseYourGuy()
	{
		isCrewed = false;
		engineSwitch.interactable = false;
		ionicCons.interactable = false;
		MakeGuyAvailableOnTimer ();
	}

	void MakeGuyAvailableOnTimer(){
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


	}

	IEnumerator MakeAGuyJoinTheRoom()
	{
		crewLeave.interactable = false;
		crewDescriptif.text = crewCharScript.nom + " is on it's way !";
		GetComponent<PlayerInterfaceManager> ().crewSelectPanel = false;

		yield return new WaitForSeconds (3f);
		crewDescriptif.text = "No special skills.";
		engineSwitch.interactable = true;
		if ((crewCharScript.techComp == true)) 
		{
			crewDescriptif.text = "Mecanician level " + crewCharScript.lvlTechComp;
			ionicCons.interactable = true;
			crewCharScript.isAssigned = true;
		}
		crewLeave.interactable = true;

	}
	public void TempRize ()
	{
		if (isCrewed) {
			tempRoom.value += Mathf.Round (ionicCons.value / crewCharScript.lvlTechComp) + 1;
			return;
		} else {
			tempRoom.value += ionicCons.value;
		}
	}
	public void TempCool()
	{
		tempRoom.value -= coolerLvl;
		if (isCrewed) 
		{
			if (crewCharScript.techComp) {
				tempRoom.value -= coolerLvl * crewCharScript.lvlTechComp;
			}
		}
	}
}