using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewCharacter : MonoBehaviour {
		int pvMax = 10;
		int pvActuel = 10;
	public bool techComp = true;
	public int lvlTechComp = 5;
		public string nom = "Jessy Jones";
	public int ID = 5;
	public bool isAssigned = false;
	public GameObject menuResume;
	public Color colorBad;
	public Color colorGood;
	private ColorBlock thecolor;
	private GameObject playerInterface;


	enum Status
		{
			hurt,
			healthy,
			sick,
			infected
		}Status status = Status.healthy;
		
	void Start()
	{
		playerInterface = GameObject.Find ("PlayerInterface");		
		gameObject.transform.parent = playerInterface.transform;
		ActualizedTheInfoHandler ();
	}

	public void ActualizedTheInfoHandler()
	{
		CrewMemberInfoHandler crewInfo;
		crewInfo =  menuResume.GetComponent<CrewMemberInfoHandler> ();
		crewInfo.name.text = nom;
		crewInfo.selectButton.onClick.AddListener(SayImInRoomX);
		if (techComp == true) 
		{
			crewInfo.compDescription.text = "Technician level " + lvlTechComp;
				
		}
		if (isAssigned == false) {
			crewInfo.selectButton.GetComponent<Image> ().color = colorGood;
			crewInfo.description.text = "Is available";
			crewInfo.selectButton.interactable = true;

				
		}
		if (isAssigned == true) {
			crewInfo.selectButton.GetComponent<Image> ().color = colorBad;
			crewInfo.description.text = "Is not available";
			crewInfo.selectButton.interactable = false;

		}

	}	public void SayImInRoomX()
	{

		if (playerInterface.GetComponent<PlayerInterfaceManager> ().motorRoomPanel == true) {
			gameObject.transform.parent.GetComponent<MotorInterface> ().GetAGuy (gameObject.name);
		}
		if (playerInterface.GetComponent<PlayerInterfaceManager> ().TechnicalBayPanel == true) 
		{
			playerInterface.GetComponent<TechnicalBayInterface> ().GetAGuy (gameObject.name);

		}
	}
}

