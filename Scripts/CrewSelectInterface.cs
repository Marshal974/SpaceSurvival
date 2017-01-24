using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewSelectInterface : MonoBehaviour {

	public bool actifTab;
	public GameObject ChildMenu;
	public CrewCharacter[] crewChar;
	public GameObject CrewPanel;
	public GameObject CrewCharPrefab;
	private int IDofCrew = 1;


	void Awake()
	{
		crewChar = gameObject.GetComponentsInChildren<CrewCharacter> ();

		foreach(CrewCharacter crew in crewChar)
		{
//			if (crew.isAssigned == false) {
				crew.ID = IDofCrew;
				IDofCrew += 1;
				GameObject Go = Instantiate (CrewCharPrefab, CrewPanel.transform, false);
				Go.name = "crewmember " + crew.ID ;
				crew.menuResume = Go.gameObject;
//			}
		}
	}

	public void ToggleMenu(){

		if (actifTab == false){
			
			ChildMenu.SetActive (true);
			GetComponent<PlayerInterfaceManager> ().crewSelectPanel = true;
			crewChar = gameObject.GetComponentsInChildren<CrewCharacter> ();

			foreach (CrewCharacter crew in crewChar) {
				crew.ActualizedTheInfoHandler ();
			}

			actifTab = true;
			return;
		}
		if (actifTab == true){
			GetComponent<PlayerInterfaceManager> ().crewSelectPanel = false;
			ChildMenu.SetActive (false);
			actifTab = false;	


						
		}

	}
		

}