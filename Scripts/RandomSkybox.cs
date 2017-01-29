using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkybox : MonoBehaviour {

	public Material[] skyboxes;
	// Use this for initialization
	void Awake () {
		RenderSettings.skybox = skyboxes [Random.Range (0, skyboxes.Length)];
	}
	
	public void ChangeTheSkybox()
	{
		RenderSettings.skybox = skyboxes [Random.Range (0, skyboxes.Length)];

	}
}
