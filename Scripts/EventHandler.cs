using UnityEngine;
using System.Collections;

public class EventHandler : MonoBehaviour {

	public int missionNumber;

	public void DontTouchIt(){
		DontDestroyOnLoad(this.gameObject);
	}

//	public void MissionChoose(){
//		missionNumber = GetComponent(mission).script
//	}

}
