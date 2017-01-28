using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewCharacter : MonoBehaviour {
	int pvMax = 10;
	int pvActuel = 10;
    public bool techComp = true;
    public int lvlTechComp = 5;

    public bool medComp = true;
    public int lvlMedComp = 5;

    public bool navComp;
	public int lvlNavComp = 5;
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
		}
    Status status = Status.healthy;
		
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
		crewInfo.compDescription.text = "";

		if (!techComp && !navComp && !medComp) 
		{
			crewInfo.compDescription.text = "No special skills.";
		}
		if (techComp == true) 
		{
			crewInfo.compDescription.text += "Technician level " + lvlTechComp + "  ";
				
		}
        if (navComp)
        {
            crewInfo.compDescription.text += "Navigator level " + lvlNavComp + "  ";
        }
        if (medComp)
        {
            crewInfo.compDescription.text += "Medecin level " + lvlNavComp;

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

	}

    public void SayImInRoomX()
	{

		if (playerInterface.GetComponent<PlayerInterfaceManager> ().motorRoomPanel == true) {
			gameObject.transform.parent.GetComponent<MotorInterface> ().GetAGuy (gameObject.name);
		}
		if (playerInterface.GetComponent<PlayerInterfaceManager> ().TechnicalBayPanel == true) 
		{
			playerInterface.GetComponent<TechnicalBayInterface> ().GetAGuy (gameObject.name);

		}
        if (playerInterface.GetComponent<PlayerInterfaceManager>().radarRoomPanel == true)
        {
            playerInterface.GetComponent<RadarInterface>().GetAGuy(gameObject.name);
        }

        if (playerInterface.GetComponent<PlayerInterfaceManager>().resRoomPanel == true)
        {
            playerInterface.GetComponent<ResRoomInterface>().GetAGuy(gameObject.name);
        }
    }
}

