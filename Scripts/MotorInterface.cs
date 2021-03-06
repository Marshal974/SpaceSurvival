﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotorInterface : MonoBehaviour {
    // coup d'énergie pour redémarrer si jamais elle est éteinte : quand plus d'énergie
    public int ModuleBootCost = 200;
	public Button moduleReboot;

	public GameObject motorMessPrefab;

	public bool actifTab; // fenêtre ouverte ou pas

	public GameObject ChildMenu;
	public Slider ionicCons;
	public Slider tempRoom;
	public Text ionicSpeed;
	public Text ElecConsTxt; // affiche les informations pour le joueur
	public Button engineSwitch; // en cours

	public Text coolingMsg; 
	public Text coolingLvlMsg;
	public Button coolingBtn;
	public int coolerLvl = 1;
	private int _coolerLvl;

	private bool overheating; // en surchauffe ?
	private bool isShutdown; // module éteint ou allumer
	public Text advertiseTxt;


	public GameObject CrewPanel;
	public Text crewName;
	public Text crewDescriptif;
	private CrewCharacter crewCharScript;
	public Button assignCrew;
	public Button crewLeave;
	public bool isCrewed;
	private PlayerShipStats playerShipStats;
	private bool alreadyRunning; // permet de savoir si un script est déjà instancier ou non


	public void Start()
	{
		coolingMsg.text = "Cooling systems level " + coolerLvl + " are operational.";
		playerShipStats = GameObject.Find ("PlayerObj").GetComponent<PlayerShipStats> ();


	}

	public void Update()
	{
		if (overheating == false && tempRoom.value == tempRoom.maxValue) 
		{
			GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
			go.GetComponentInChildren<Image> ().color = Color.red;
			go.GetComponentInChildren<Text> ().text = "The engines are overheating! max speed reduced.";
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
				GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
				go.GetComponentInChildren<Image> ().color = Color.green;
				go.GetComponentInChildren<Text> ().text = "The engines are ready to be pushed again !";
				overheating = false;
				ionicCons.maxValue = 30;

			}
		}
	}


	public void BreakTheModule()
	{
		GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
		go.GetComponentInChildren<Image> ().color = Color.red;
		go.GetComponentInChildren<Text> ().text = "Something is going really wrong in the engines room. Better fix it.";
		_coolerLvl = coolerLvl;
		coolerLvl = 0;
		coolingBtn.interactable = true;
		coolingBtn.gameObject.GetComponent<Image> ().color = Color.red;
		if ((crewCharScript.techComp == true)) 
		{			
			ionicCons.interactable = false;
			engineSwitch.interactable = false;

		}
	}

	public void FixTheModule()
	{
		GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
		go.GetComponentInChildren<Image> ().color = Color.red;
		go.GetComponentInChildren<Text> ().text = "The Engines have been fixed. Great job.";
		coolerLvl = _coolerLvl;
		coolingBtn.interactable = false;
		coolingBtn.gameObject.GetComponent<Image> ().color = Color.white;
		if ((crewCharScript.techComp == true)) 
		{			
			ionicCons.interactable = true;
			engineSwitch.interactable = true;

		}
	}

	public void ToggleMenu(){
		GetComponent<PlayerInterfaceManager>().ChangeCamPosition ();
		;
		if (actifTab == false){
			GetComponent<PlayerInterfaceManager> ().CloseAllOtherModules ();
			GetComponent<PlayerInterfaceManager>().motorRoomPanel =! GetComponent<PlayerInterfaceManager>().motorRoomPanel;

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
			GetComponent<PlayerInterfaceManager>().motorRoomPanel =! GetComponent<PlayerInterfaceManager>().motorRoomPanel;

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


	//calcul et affichage de la consommation du module
	public void SetMotorConsRate()
	{

		playerShipStats.motorPowerCons = Mathf.RoundToInt ( ionicCons.value);
		playerShipStats.audioS.volume =0.2f+0.01f * ionicCons.value;
		GameObject.Find ("PlayerObj").GetComponent<ClicToMove> ().speed = ionicCons.value/2;
		ionicSpeed.text = "Ionic speed: " + ionicCons.value / 2 + " m/s.";
		ElecConsTxt.text = ionicCons.value.ToString() + "/s.";
	}

	//arret du module ! (appeler quand ya pu de courant par exemple)
	public void StopMotorModule()
	{
		GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
		go.GetComponentInChildren<Image> ().color = Color.red;
		go.GetComponentInChildren<Text> ().text = "The engines are shutdown.";
		playerShipStats.audioS.mute = true;

		isShutdown = true;
		ionicCons.value = 0;
		engineSwitch.interactable = false;
		ionicCons.interactable = false;
		moduleReboot.GetComponentInChildren<Text>().text = "Engines are shutdown. Need "+ ModuleBootCost +" power to restart.";
		moduleReboot.gameObject.SetActive (true);


	}

	//redémarrage du module !
	public void StartMotorModule()
	{
		if (playerShipStats.shipPower > ModuleBootCost) {
			isShutdown = false;
			StartCoroutine (EngineSwitchOn ());
			moduleReboot.gameObject.SetActive (false);
				return;
		}
		advertiseTxt.text = "Not enough power to restart the engines.";
			

	}

	IEnumerator EngineSwitchOn()
	{
		advertiseTxt.text = "Starting the engines !";
		playerShipStats.audioS.clip = playerShipStats.engineRestartSnd;
		playerShipStats.audioS.volume = 1f;
		playerShipStats.audioS.mute = false;
		playerShipStats.audioS.Play ();
			
		playerShipStats.shipPower -= ModuleBootCost / 5;
		yield return new WaitForSeconds (1f);
		playerShipStats.shipPower -= ModuleBootCost / 5;
		yield return new WaitForSeconds (1f);
		advertiseTxt.text = "Almost Online";
		playerShipStats.shipPower -= ModuleBootCost / 5;
		yield return new WaitForSeconds (1f);
		playerShipStats.shipPower -= ModuleBootCost / 5;
		yield return new WaitForSeconds (1f);

		advertiseTxt.text = "Engines are ready";
		playerShipStats.shipPower -= ModuleBootCost / 5;
		ionicCons.value = 5;

		if (isCrewed) {
			engineSwitch.interactable = true;

			if (crewCharScript.techComp == true) {
				ionicCons.interactable = true;
			}
		}
		yield return new WaitForSeconds (1f);
		playerShipStats.audioS.mute = true;

		advertiseTxt.text = "";
		GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
		go.GetComponentInChildren<Image> ().color = Color.green;
		go.GetComponentInChildren<Text> ().text = "Propulsion System back online.";
		

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
		GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
		go.GetComponentInChildren<Image> ().color = Color.white;
		go.GetComponentInChildren<Text> ().text = crewCharScript.nom + "is now available.";


	}

	IEnumerator MakeAGuyJoinTheRoom()
	{
		alreadyRunning = true;
		crewCharScript.isAssigned = true;
		crewLeave.interactable = false;
		crewDescriptif.text = crewCharScript.nom + " is on it's way !";
		GetComponent<PlayerInterfaceManager> ().crewSelectPanel = false;

		yield return new WaitForSeconds (3f);
		crewDescriptif.text = "No special skills.";

		if (isShutdown == false) 
		{
			engineSwitch.interactable = true;
		}
		
		if ((crewCharScript.techComp == true)) 
		{
			crewDescriptif.text = "Mecanician level " + crewCharScript.lvlTechComp;
			if (isShutdown == false) {
				ionicCons.interactable = true;
			}
		}
		crewLeave.interactable = true;
		GameObject go = Instantiate (motorMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
		go.GetComponentInChildren<Image> ().color = Color.white;
		go.GetComponentInChildren<Text> ().text = crewCharScript.nom + " is now active in the engines room.";

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