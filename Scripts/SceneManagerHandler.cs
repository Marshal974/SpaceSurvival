using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerHandler : MonoBehaviour {

	//GameObjects references in the scene :
	public GameObject MessPrefab;
	public GameObject alertMessPanel;
	private GameObject playerObj;
	private GameObject playerInterface;
	public Transform interestPointPanel;

	//prefabs that can be spawn at scene start :
	public GameObject sunPrefab;
	public GameObject wreckagePrefab;
	public GameObject planetPrefab;
	public GameObject asteroidPrefab;
	public GameObject satellitePrefab; // faire un satellite qui se déplace a genre 10m/s et faut l'intercepter / puis rester a porter un temps pour en prendre le controle ce qui le stop / et qui declenche une mission de loot / expedition.

	//used for start process: 
	private int rndNumber;


	void Start () 
	{
		playerInterface = GameObject.Find ("PlayerInterface");
		playerObj = GameObject.Find ("PlayerObj");
		alertMessPanel = GameObject.Find ("AlertMessagePanel");
		Camera.main.gameObject.GetComponent<RandomSkybox> ().ChangeTheSkybox ();
		playerObj.GetComponent<PlayerShipStats> ().shipRadLevel = 0;
		rndNumber = Random.Range (0, 101);

		// low chance to happen
		if (rndNumber < 20) 
		{
			AddAnAsteroid ();
		}
		// one chance on two to happen
		if (rndNumber < 50) 
		{
			Instantiate (sunPrefab);
			AddASatellite ();
			BreakTheMotorRoom ();

		}
		// high chance to happen
		if (rndNumber < 70) 
		{
			RadiationActivated ();
			LeakOfOxygen ();

		}
		if (rndNumber == 100) 
		{
			AddAPlanet ();
		}
		AddAWreckage ();
		Instantiate (sunPrefab);

	}


	//list of possible events to choose from when starting the scene.

	public void BreakTheMotorRoom()
	{
		playerInterface.GetComponent<MotorInterface> ().BreakTheModule ();

	}

	public void AddAWreckage()
	{
		GameObject go = Instantiate (wreckagePrefab);
		go.transform.position = playerObj.transform.position + new Vector3 (Random.Range (-2000, 2000), 0, Random.Range (-2000, 2000));

	}
	public void AddAPlanet ()
	{
		GameObject go = Instantiate (planetPrefab);
	}

	public void AddAnAsteroid ()
	{
		GameObject go = Instantiate (asteroidPrefab);
	}
		
	public void AddASatellite ()
	{
		playerInterface.GetComponent<RadarInterface> ().AddASatellite ();
		GameObject go = Instantiate (satellitePrefab);
	}

	public void LeakOfOxygen()
	{
		playerObj.GetComponent<PlayerShipStats> ().shipO2Loses += 5;
		GameObject go = Instantiate (MessPrefab,alertMessPanel.transform,false);
		go.GetComponentInChildren<Image> ().color = Color.red ;
		go.GetComponentInChildren<Text> ().text = "ALERT LEAK OF OXYGEN IN TECHBAY ROOM.";
		playerInterface.GetComponent<TechnicalBayInterface> ().BreakTheModule ();
	}

	public void RadiationActivated()
	{
		GameObject go = Instantiate (MessPrefab,alertMessPanel.transform,false);
		go.GetComponentInChildren<Image> ().color = Color.yellow ;
		go.GetComponentInChildren<Text> ().text = "ALERT we are in a high radiation sector. Better jump out fast ! ";
		playerObj.GetComponent<PlayerShipStats> ().shipRadLevel = 1;
	}
}
