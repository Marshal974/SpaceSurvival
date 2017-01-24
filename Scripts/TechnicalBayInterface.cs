using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnicalBayInterface : MonoBehaviour {

	public bool actifTab;
	public GameObject childMenu;
	public Text crewName;
	public Text crewDescriptif;
	private CrewCharacter crewCharScript;
	public Button assignCrew;
	public Button crewLeave;
	public GameObject CrewPanel;

	public bool isCrewed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ToggleMenu(){
		GetComponent<PlayerInterfaceManager>().ChangeCamPosition ();
		GetComponent<PlayerInterfaceManager>().TechnicalBayPanel =! GetComponent<PlayerInterfaceManager>().TechnicalBayPanel;
		if (actifTab == false){

			childMenu.SetActive (true);
			actifTab = true;
			if (isCrewed) 
			{
				//				if ((gameObject.GetComponentInChildren <CrewCharacter> () as CrewCharacter) != null) {
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
		assignCrew.gameObject.SetActive (true);
		CrewPanel.SetActive (false);


	}

	IEnumerator MakeAGuyJoinTheRoom()
	{
		crewLeave.interactable = false;
		crewCharScript.isAssigned = true;

		crewDescriptif.text = crewCharScript.nom + " is on it's way !";
		GetComponent<PlayerInterfaceManager> ().crewSelectPanel = false;

		yield return new WaitForSeconds (3f);
		crewDescriptif.text = "No special skills.";

		if ((crewCharScript.techComp == true)) 
		{
			crewDescriptif.text = "Mecanician level " + crewCharScript.lvlTechComp;

		}
		crewLeave.interactable = true;

	}

}
