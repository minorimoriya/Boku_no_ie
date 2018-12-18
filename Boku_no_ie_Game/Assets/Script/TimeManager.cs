using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public Text timerText;

    public float totaltime;
    int seconds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(GlobalParameters.kakureta == false){
            CountDown();
        }

	}

    void CountDown(){
        totaltime -= Time.deltaTime;//フレーム-フレームの秒数
        seconds = (int)totaltime;
        timerText.text = seconds.ToString();//ToStringで文字列にする

        if (seconds == 0f)
        {
            timerText.gameObject.SetActive(false);
        }

    }
}
