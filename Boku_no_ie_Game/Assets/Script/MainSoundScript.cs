using System.Collections;
using UnityEngine;

public class MainSoundScript : MonoBehaviour {
	public bool DontDestroyEnabled = true;
	
	// Use this for initialization
	void Start () {
		if (DontDestroyEnabled) {
			//Sceneがとんでも音が消えないようにする
			DontDestroyOnLoad (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		public void StopBGM(){
		gameObject.GetComponent<AudioSource> ().Stop ();
	}
}

