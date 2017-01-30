using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewSelectInterface : MonoBehaviour {

	public bool actifTab;
	public GameObject ChildMenu;

	public List<CrewCharacter> crewCharList =new List<CrewCharacter>() ;
	public CrewCharacter[] crewChar ;
	public GameObject CrewPanel;
	public GameObject CrewCharPrefab;
	private int IDofCrew = 1;



	void Awake()
	{
		crewChar = gameObject.GetComponentsInChildren<CrewCharacter> ();
		crewCharList = new List<CrewCharacter> (crewChar);

		foreach(CrewCharacter crew in crewCharList)
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


	public void AddAPlayerSelectButton(CrewCharacter crew)
	{
		crewCharList.Add (crew);
		crew.ID = IDofCrew;
		IDofCrew += 1;
		GameObject Go = Instantiate (CrewCharPrefab, CrewPanel.transform, false);
		Go.name = "crewmember " + crew.ID ;
		crew.menuResume = Go.gameObject;
	}

	public void ToggleMenu(){
		GetComponent<PlayerInterfaceManager>().PlayClicAcceptedSound();


		if (actifTab == false){
			
			ChildMenu.SetActive (true);
			GetComponent<PlayerInterfaceManager> ().crewSelectPanel = true;

			foreach (CrewCharacter crew in crewCharList) {
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