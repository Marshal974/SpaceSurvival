using UnityEngine;
using System.Collections;

public class ButtonSound : MonoBehaviour {

	public AudioClip myclip;

	// Use this for initialization
	void Start ()
	{
		this.gameObject.AddComponent<AudioSource>();
		this.GetComponent<AudioSource>().clip = myclip;
		this.GetComponent<AudioSource>().Play();
	}
}
