using UnityEngine;
using System.Collections;

public class SceneEvent : MonoBehaviour {
	public int timeToGo;
	public int actualTime;
	public GameObject GoButton;
	public GameObject Pod1;
	public GameObject Pod2;
	public bool podChoice;
	public GameObject[] CrewChoice;
	public bool CrewModuleChoice;

	public void PodChoice(){
		//select the pod Module
		
		podChoice = !podChoice;
		Pod1.SetActive (podChoice);
		Pod2.SetActive (!podChoice);
		if (Pod1.activeInHierarchy == true) {
			actualTime = 3;
		} else if (Pod2.activeInHierarchy == true) {
			actualTime = 4;
		}
	}
	public void CrewModule() {
		//select the crew module
		CrewModuleChoice = !CrewModuleChoice;
		CrewChoice [0].SetActive (CrewModuleChoice);
		CrewChoice [1].SetActive (!CrewModuleChoice);
	
	}

	void Update()
	{
		if (actualTime < timeToGo && actualTime>3) {
			GoButton.SetActive (true);
		}
			else
			{
			GoButton.SetActive(false);
			}
		}
	}

