using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBGM : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject bgm = GameObject.Find ("BGM");
		if (bgm != null) {
			bgm.GetComponent<MainSoundScript> ().StopBGM ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
