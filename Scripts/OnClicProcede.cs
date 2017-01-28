using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClicProcede : MonoBehaviour {

	bool alreadyIntroducedToP;

	public GameObject alertMsgPrefab;
	public PlayerInterfaceManager playerIntManager;

	public void OnClicEvent()
	{
		if (!alreadyIntroducedToP) {
			alreadyIntroducedToP = true;

			GameObject go = Instantiate (alertMsgPrefab, playerIntManager.alertMessPanel, false);
			go.GetComponentInChildren<Image> ().color = Color.green;
			go.GetComponentInChildren<Text> ().text = "An old ship wreck has been spotted close by.";
		}
	}
}
