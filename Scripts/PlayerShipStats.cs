using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShipStats : MonoBehaviour {
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
	private float consRate = 1f;
	public int motorPowerCons;
	public Text powerText;

	public int shipMaxOxy;
	public int shipOxy;
	public Text oxyText;

	public int shipOxyRenewRate;
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
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time > ticTimer) {
			ticTimer = Time.time + consRate;
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


		oxyText.text = shipOxy.ToString() + " / " + shipMaxOxy.ToString();
		integrityText.text = shipIntegrity.ToString();
		powerText.text = shipPower.ToString() + " / " + shipMaxPower.ToString();
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
		shipPower = 0;
		playerInterfaceUI.GetComponent<MotorInterface> ().StopMotorModule ();
	}
		
}
