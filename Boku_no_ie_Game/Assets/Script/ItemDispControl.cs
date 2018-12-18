using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispControl : MonoBehaviour {
    public GameObject hasami;
    public GameObject Bat;
    public GameObject kagi;
	
    // Use this for initialization
	void Start () {
        if(GlobalParameters.keyImage == true){
            hasami.SetActive(true);
        }else{
            hasami.SetActive(false);
        }

        if (GlobalParameters.keyImage2 == true)
        {
            Bat.SetActive(true);
        }
        else
        {
            Bat.SetActive(false);
        }

        if (GlobalParameters.keyImage3 == true)
        {
            kagi.SetActive(true);
        }
        else
        {
            kagi.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
