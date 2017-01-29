using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInterfaceManager : MonoBehaviour {

	public bool crewSelectPanel;
	public bool motorRoomPanel;
	public bool TechnicalBayPanel;
    public bool radarRoomPanel;
    public bool resRoomPanel;
    public Transform alertMessPanel;
//	public Button techBBtn;
//	public Button MotorRBtn;
//	public Button radarRBtn;
	public Camera camShip;
	public Transform[] camPositions;

	public UnityEvent ToggleMotorPanel;
	public UnityEvent ToggleTechBayPanel;
    public UnityEvent ToggleRadarRoomPanel;
    public UnityEvent ToggleResRoomPanel;

    public bool globalShipView;
	// Use this for initialization

	// Sound management
	public AudioSource audioSMenu;
	public AudioClip clicDenySnd;
	public AudioClip clicAcceptedSnd;

	void Start () {
		camShip = GameObject.Find ("ShipCamera").GetComponent<Camera> ();
		audioSMenu = GetComponent<AudioSource> ();
	}
	
	public void PlayClicAcceptedSound()
	{
		audioSMenu.PlayOneShot (clicAcceptedSnd);
	}
	public void PlayClicDeniedSound()
	{
		audioSMenu.PlayOneShot (clicDenySnd);
	}
	public void ChangeCamPosition()
	{
		StartCoroutine (ChangeCamView ());
	}
	IEnumerator ChangeCamView(){

		yield return new WaitForEndOfFrame ();
		if (globalShipView == false) 
		{
			globalShipView = true;
			if (motorRoomPanel == true) 
			{
				camShip.fieldOfView = 5;
//				techBBtn.interactable = false;
				yield break;

			}
			if (TechnicalBayPanel == true) 
			{
				camShip.fieldOfView = 8;
//				MotorRBtn.interactable = false;
				yield break;

			}
            if (radarRoomPanel)
            {
                camShip.fieldOfView = 10;
                yield break;
            }
            if (resRoomPanel)
            {
                camShip.fieldOfView = 3;
                yield break;
            }
        }
//		techBBtn.interactable = true;
//		MotorRBtn.interactable = true;
		globalShipView = false;
		camShip.fieldOfView = 60;

	}

	public void CloseAllOtherModules()
	{
		if (motorRoomPanel) {
			ToggleMotorPanel.Invoke ();
		}
		if (TechnicalBayPanel) {
			ToggleTechBayPanel.Invoke ();
		}
        if (radarRoomPanel)
        {
            ToggleRadarRoomPanel.Invoke();
        }
        if (resRoomPanel)
        {
            ToggleResRoomPanel.Invoke();
        }
    }



}
