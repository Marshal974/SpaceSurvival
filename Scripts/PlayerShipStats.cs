using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShipStats : MonoBehaviour {
	GameObject shipStatMessPrefab;
	public int shipMaxIntegrity = 100;
	public int shipIntegrity = 100;
	public Text integrityText;

	public int shipResisToRad = 5;
	public int shipTotalCrew;
	public Text crewText;

	public int shipTotalPassengers;
	public Text passengersText;

	public int shipMaxPower;
	public int shipPower;
	public int shipPowerRegen;
	public int shipPowerLoses;
	private float consRate = 1f;
	public int motorPowerCons;
	public Text powerText;

	public int shipMaxOxy;
	public int shipOxy;
	public int shipO2Loses;
	public Text oxyText;

	public int shipOxyRenewRate = 1;
	public int shipfilterOxyLvl = 1;
	public int rocketParts;
	public int maxrocketParts;
	public float shipMaxFuel;
	public float shipFuel;
	public Text fuelText;


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
		if (Time.time > ticTimer) {
			
			ticTimer = Time.time + consRate;
			shipOxy += (shipOxyRenewRate * shipfilterOxyLvl);
			shipOxy -= shipTotalCrew;
			shipPower += shipPowerRegen;
			shipPower -= shipPowerLoses;
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
		GameObject go = Instantiate (shipStatMessPrefab, GetComponent<PlayerInterfaceManager> ().alertMessPanel, false);
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
}
