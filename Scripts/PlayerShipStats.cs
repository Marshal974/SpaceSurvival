using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Script qui permet de gérer les statistiques du vaisseau
/// Souvent appelle dans d'autres modules
/// </summary>

public class PlayerShipStats : MonoBehaviour {
    // fais aparaitre un message
    // Notification ingame
	public GameObject shipStatMessPrefab;


    // pv
	public int shipMaxIntegrity = 100;
	public int shipIntegrity = 100;
	public Text integrityText;

    // résistances aux radiations
	public int shipResisToRad = 5;
	public int shipTotalCrew;
	public Text crewText;

    // nombre max de passengers
	public int shipTotalPassengers;
	public Text passengersText;

    // Energie du vaisseau 
    // Power / MaxPower / Regen
	public int shipMaxPower;
	public int shipPower;
	public int shipPowerRegen;
	public int shipPowerLoses;
	private float consRate = 1f;
	public int motorPowerCons;
	public Text powerText;

    // Oxygen part
	public int shipMaxOxy;
	public int shipOxy;
	public int shipO2Loses;
	public Text oxyText;
	private bool O2AlertDone; //is the alert for "O2 at zero" sent ? 
	private bool o2AboveTenth; // is the alert for "o2 above 10% " sent ? 
	public float timeBeforeSuffocate; // how long can the air stay breathable without new o2 ? 
	private float officialTimeToSuff;
	private bool isSuffocating;

	public int shipOxyRenewRate = 1;
	public int shipfilterOxyLvl = 1;
	public int rocketParts;
	public int maxrocketParts;
	public float shipMaxFuel;
	public float shipFuel;
	public float shipFuelRegen = 1;
	public Text fuelText;
	public Slider fuelInMotorRoom;
	public bool readyToJumpAlert; // get set to false by the JumpProcedure of the MotorInterfaceScript

	//sons
	public AudioSource audioS;
	public AudioClip engineSnd;
	public AudioClip engineRestartSnd;



	public bool isMoving = false;
	public GameObject  playerInterfaceUI;

	private float ticTimer;
	// Use this for initialization
	void Start () {
		shipfilterOxyLvl = playerInterfaceUI.GetComponent<TechnicalBayInterface> ().filterLvl;
		TellTechBayRegenRate ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shipOxy == 0 && Time.time > officialTimeToSuff && isSuffocating) 
		{
			Debug.Log ("kill the crew"); 
			isSuffocating = false;
			// add here events for what happen when dying because of air scarecity
		}

		if (O2AlertDone) 
		{
			if (shipOxy > (shipMaxOxy / 100) && o2AboveTenth == false) 
			{
				isSuffocating = false;
				o2AboveTenth = true;
				GameObject go = Instantiate (shipStatMessPrefab, playerInterfaceUI.GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
				go.GetComponentInChildren<Image> ().color = Color.yellow;
				go.GetComponentInChildren<Text> ().text = "Oxygen reserves critical but people are breathing again.";
				O2AlertDone = false;
				oxyText.color = Color.white;
			}
				

		}
		if (Time.time > ticTimer) {
			
			ticTimer = Time.time + consRate;
			shipOxy += (shipOxyRenewRate * shipfilterOxyLvl);
			shipOxy -= shipTotalCrew;
			shipPower += shipPowerRegen;
			shipPower -= shipPowerLoses;
			shipFuel += shipFuelRegen;
			fuelInMotorRoom.value = shipFuel;
			if (playerInterfaceUI.GetComponent<MotorInterface> ().tempRoom.value > 0) {
				playerInterfaceUI.GetComponent<MotorInterface> ().TempCool ();
			}
			if (isMoving == true) {

				PowerCons ();
				playerInterfaceUI.GetComponent<MotorInterface> ().TempRize ();
			}
		}
		if (shipIntegrity > shipMaxIntegrity) 
		{
			shipIntegrity = shipMaxIntegrity;
		}

		if (shipOxy > shipMaxOxy) 
		{
			shipOxy = shipMaxOxy;
		}
		if (shipOxy <= 0) {
			shipOxy = 0;
			if (O2AlertDone == false) {
				o2AboveTenth = false;
				oxyText.color = Color.red;
				O2AlertDone = true;
				officialTimeToSuff = Time.time + timeBeforeSuffocate;
				isSuffocating = true;
				GameObject go = Instantiate (shipStatMessPrefab, playerInterfaceUI.GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
				go.GetComponentInChildren<Image> ().color = Color.red;
				go.GetComponentInChildren<Text> ().text = "We are out of fresh air. people are gonna die here !!!";
			}
		}
		if (shipPower > shipMaxPower) 
		{
			shipPower = shipMaxPower;
		}
		if (shipPower < 0) 
		{
			ShutdownTheShipMods ();
		}

		if (shipFuel > shipMaxFuel) 
		{
			shipFuel = shipMaxFuel;
			if (readyToJumpAlert == false) 
			{
				readyToJumpAlert = true;
				GameObject go = Instantiate (shipStatMessPrefab, playerInterfaceUI.GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
				go.GetComponentInChildren<Image> ().color = Color.green;
				go.GetComponentInChildren<Text> ().text = "We have enough antimatter to make the jump.";
				if (playerInterfaceUI.GetComponent<MotorInterface> ().tempRoom.value > 1000) 
				{
					GameObject go2 = Instantiate (shipStatMessPrefab, playerInterfaceUI.GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
					go2.GetComponentInChildren<Image> ().color = Color.yellow;
					go2.GetComponentInChildren<Text> ().text = "Unfortunatly...the engines are too hot for jumping !";
					return;
				}

				playerInterfaceUI.GetComponent<MotorInterface> ().engineSwitch.gameObject.GetComponent<Image> ().color = Color.green;
			}
		}


		oxyText.text = shipOxy.ToString() + " / " + shipMaxOxy.ToString()+ " / "+ ((shipOxyRenewRate * shipfilterOxyLvl) - shipTotalCrew - shipO2Loses) ;
		integrityText.text = shipIntegrity.ToString();
		powerText.text = shipPower.ToString() + " / " + shipMaxPower.ToString()+ " / "+ (shipPowerRegen - shipPowerLoses);
		if (isMoving) 
		{
			powerText.text = shipPower.ToString() + " / " + shipMaxPower.ToString()+ " / "+ (shipPowerRegen - shipPowerLoses - motorPowerCons);

		}
		crewText.text = shipTotalCrew.ToString();
		passengersText.text = shipTotalPassengers.ToString();
	}

	void PowerCons()
	{
		shipPower -= motorPowerCons;
	// rajouter ici les autres modules qui consomme a l'avenir...
	}



	public void ShutdownTheShipMods ()
	{
		GameObject go = Instantiate (shipStatMessPrefab, playerInterfaceUI.GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
		go.GetComponentInChildren<Image> ().color = Color.red;
		go.GetComponentInChildren<Text> ().text = "We are out of power. Shutting down modules...";
		shipPower = 0;
		playerInterfaceUI.GetComponent<MotorInterface> ().StopMotorModule ();
		playerInterfaceUI.GetComponent<TechnicalBayInterface> ().StopTechBayModule ();
	}
		
	public void	TellTechBayRegenRate ()
	{
		int regenRate;
		regenRate = (shipOxyRenewRate * shipfilterOxyLvl);
		int consumpRate = shipTotalCrew;
		playerInterfaceUI.GetComponent<TechnicalBayInterface>().filterRegenO2Txt.text = "Consumption rate: "+ consumpRate +" /s." + "     Purifying rate: "+ regenRate +" /s.";
	}

	public void StartToMove()
	{
		audioS.clip = engineSnd;
		audioS.mute = false;
		audioS.Play ();


		isMoving = true;
	}

	public void StopMoving()
	{
		audioS.mute = true;
		isMoving = false;

	}
}
