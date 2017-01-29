using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerHandler : MonoBehaviour {

	public GameObject MessPrefab;
	public GameObject alertMessPanel;

	public GameObject wreckagePrefab;




	void Start () 
	{
		alertMessPanel = GameObject.Find ("AlertMessagePanel");
		Camera.main.gameObject.GetComponent<RandomSkybox> ().ChangeTheSkybox ();
		LeakOfOxygen ();
		BreakTheMotorRoom ();
		AddAWreckage ();
		
	}


	//Make a list of possible events to choose from when starting the scene.

	public void BreakTheMotorRoom()
	{
		GameObject.Find ("PlayerInterface").GetComponent<MotorInterface> ().BreakTheModule ();

	}

	public void AddAWreckage()
	{
		GameObject go = Instantiate (wreckagePrefab);
		go.transform.position = GameObject.Find ("PlayerObj").transform.position + new Vector3 (Random.Range (-2000, 2000), 0, Random.Range (-2000, 2000));
	}

	public void LeakOfOxygen()
	{
		GameObject.Find ("PlayerObj").GetComponent<PlayerShipStats> ().shipO2Loses += 5;
		GameObject go = Instantiate (MessPrefab,alertMessPanel.transform,false);
		go.GetComponentInChildren<Image> ().color = Color.red ;
		go.GetComponentInChildren<Text> ().text = "ALERT LEAK OF OXYGEN IN TECHBAY ROOM.";
		GameObject.Find ("PlayerInterface").GetComponent<TechnicalBayInterface> ().BreakTheModule ();
	}

	public void PowerGenBroken()
	{
		
	}
}
