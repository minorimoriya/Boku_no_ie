using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadescenebutton : MonoBehaviour {
	public string scenename;
	public bool clearPlayerPref = false;
	public float FadeTime;//フェードアウトのスピードを調整、intが整数、floatが小数点まで可能

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void changeScene()
	{
		if (clearPlayerPref) {
			KeyManager.clearKey ();
		}
		SceneNavigator.Instance.Change(scenename, FadeTime);
	}
}
