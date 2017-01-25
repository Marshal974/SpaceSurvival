using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnicalBayInterface : MonoBehaviour {
	public int moduleLvl = 5;
	public int moduleConsumption = 1;
	public bool actifTab;
	public GameObject childMenu;
	public Text crewName;
	public Text crewDescriptif;
	private CrewCharacter crewCharScript;
	public Button assignCrew;
	public Button crewLeave;
	public GameObject CrewPanel;

	public Text filterLvlTxt;
	public Text filterRegenO2Txt;
	public int filterLvl;

	public Text powerGridTxt;
	public int powerIsoRegen;

	public bool isCrewed;
	private bool alreadyRunning;

	private PlayerShipStats playerShipStats;


	// Use this for initialization
	void Start () {
		powerIsoRegen = moduleLvl * 3;
		filterLvlTxt.text = "oxygen filter level " + filterLvl + " operational and clean";
		playerShipStats = GameObject.Find ("PlayerObj").GetComponent<PlayerShipStats> ();
		powerGridTxt.text = "PowerGrid level " + moduleLvl + ". We can store " + playerShipStats.shipMaxPower + " power units and our IsoGenerator is producing a constant " + powerIsoRegen + "units /s.";
		playerShipStats.shipPowerLoses += moduleConsumption;
		playerShipStats.shipPowerRegen += powerIsoRegen;

	}

		

	public void ToggleMenu(){
		GetComponent<PlayerInterfaceManager>().ChangeCamPosition ();
		GetComponent<PlayerInterfaceManager>().TechnicalBayPanel =! GetComponent<PlayerInterfaceManager>().TechnicalBayPanel;
		if (actifTab == false){

			childMenu.SetActive (true);
			actifTab = true;
			if (isCrewed) 
			{
				if ((crewCharScript.techComp == true)) 
				{

				}

			}

			return;
		}
		if (actifTab == true){

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
		playerShipStats.shipOxyRenewRate /= crewCharScript.lvlTechComp;
		playerShipStats.TellTechBayRegenRate ();
		assignCrew.gameObject.SetActive (true);
		CrewPanel.SetActive (false);


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

		if ((crewCharScript.techComp == true)) 
		{
			crewDescriptif.text = "Mecanician level " + crewCharScript.lvlTechComp;
			playerShipStats.shipOxyRenewRate *= crewCharScript.lvlTechComp;
			playerShipStats.TellTechBayRegenRate ();


		}
		alreadyRunning = false;
	}

	public void StopTechBayModule()
	{
		
	}
}
