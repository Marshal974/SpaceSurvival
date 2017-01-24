using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject[] missions;
	public GameObject[] missionsTitle;
	public bool isShowing;


	public void Start(){
		missions [1].SetActive (false);
		missions [0].SetActive (true);
		missionsTitle [1].SetActive (false);
		missionsTitle [0].SetActive (true);
	}

	public void StartMissionLevel (){

		SceneManager.LoadScene (1);
	}
	public void StartPrelaunchLevel (){

		SceneManager.LoadScene (2);
	}
	public void MenuLevel (){

		SceneManager.LoadScene (0);
	}
	public void LeaveGame(){
		Application.Quit ();
	}

	public void ChooseMission (){

		isShowing = !isShowing;
		missions[0].SetActive (isShowing);
		missions [1].SetActive (!isShowing);
		missionsTitle[0].SetActive (isShowing);
		missionsTitle[1].SetActive (!isShowing);
	}
}
