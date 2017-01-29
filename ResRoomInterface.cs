using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResRoomInterface : MonoBehaviour {
    public bool resRoomActive;
    public int moduleLvl = 5;
    public int moduleConsumption = 1;
    public bool actifTab;
    public GameObject ResMessPrefab;
    public GameObject childMenu;
    public Text crewName;
    public Text crewDescriptif; 
    private CrewCharacter crewCharScript;
    public Button assignCrew;
    public Button crewLeave;
    public GameObject CrewPanel;

    public float timeBetweenTics = 2;
    public bool isCrewed;

    private float nextTic;
    private bool alreadyRunning;

    void Start()
    {
        nextTic = Time.time;
    }
    void Update()
    {
        if (Time.time > nextTic)
        {
            while (resRoomActive)
            {
                nextTic = Time.time + timeBetweenTics;
                return;
            }
        }
    }

   

    public void ToggleMenu()
    {
        GetComponent<PlayerInterfaceManager>().ChangeCamPosition();
        if (actifTab == false)
        {
            GetComponent<PlayerInterfaceManager>().CloseAllOtherModules();
            GetComponent<PlayerInterfaceManager>().resRoomPanel = !GetComponent<PlayerInterfaceManager>().resRoomPanel;

            childMenu.SetActive(true);
            actifTab = true;
            if (isCrewed)
            {


            }

            return;
        }
        if (actifTab == true)
        {
            GetComponent<PlayerInterfaceManager>().resRoomPanel = !GetComponent<PlayerInterfaceManager>().resRoomPanel;

            childMenu.SetActive(false);
            actifTab = false;

            if (GetComponent<PlayerInterfaceManager>().crewSelectPanel == true)
            {
                GetComponent<CrewSelectInterface>().ToggleMenu();
            }



        }

    }
    //make the module Crewed
    public void GetAGuy(string name)
    {
        isCrewed = true;
        CrewPanel.SetActive(true);
        assignCrew.gameObject.SetActive(false);
        gameObject.GetComponent<CrewSelectInterface>().CrewPanel.SetActive(false);
        crewCharScript = GameObject.Find(name).GetComponent<CrewCharacter>();
        crewName.text = crewCharScript.nom;

        gameObject.GetComponent<CrewSelectInterface>().actifTab = false;
        if (alreadyRunning)
        {
            return;
        }

        StartCoroutine(MakeAGuyJoinTheRoom());
    }
    public void LooseYourGuy()
    {
        isCrewed = false;

        MakeGuyAvailableOnTimer();
    }

    public void MakeGuyAvailableOnTimer()
    {
        StartCoroutine(MakeAvailableOnT());
    }

    IEnumerator MakeAvailableOnT()
    {
        crewDescriptif.text = crewCharScript.nom + " is packing.";
        yield return new WaitForSeconds(1f);
        crewDescriptif.text = crewCharScript.nom + " is packing...";
        yield return new WaitForSeconds(1f);
        crewDescriptif.text = crewCharScript.nom + " is getting ready to leave...";
        yield return new WaitForSeconds(1f);
        crewDescriptif.text = crewCharScript.nom + " is leaving the module.";
        yield return new WaitForSeconds(2f);
        crewCharScript.isAssigned = false;
        assignCrew.gameObject.SetActive(true);
        CrewPanel.SetActive(false);
        GameObject go = Instantiate(ResMessPrefab, GetComponent<PlayerInterfaceManager>().alertMessPanel, false);
        go.GetComponentInChildren<Image>().color = Color.green;
        go.GetComponentInChildren<Text>().text = crewCharScript.nom + " is now available.";


    }

    IEnumerator MakeAGuyJoinTheRoom()
    {
        alreadyRunning = true;
        crewLeave.interactable = false;
        crewCharScript.isAssigned = true;

        crewDescriptif.text = crewCharScript.nom + " is on it's way !";
        GetComponent<PlayerInterfaceManager>().crewSelectPanel = false;

        yield return new WaitForSeconds(3f);
        crewDescriptif.text = "No special skills.";
        crewLeave.interactable = true;

        if ((crewCharScript.medComp == true))
        {
            crewDescriptif.text = "Medic level " + crewCharScript.lvlNavComp;
            timeBetweenTics = Mathf.Round(5 / crewCharScript.lvlNavComp);
        }
        alreadyRunning = false;
        GameObject go = Instantiate(ResMessPrefab, GetComponent<PlayerInterfaceManager>().alertMessPanel, false);
        go.GetComponentInChildren<Image>().color = Color.white;
        go.GetComponentInChildren<Text>().text = crewCharScript.nom + " is in the res room.";
    }

}

