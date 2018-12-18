using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovie : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("calling PlayFullScreenMovie");
        Handheld.PlayFullScreenMovie("Douga", Color.black, FullScreenMovieControlMode.CancelOnInput);

    }

    // Update is called once per frame
    void Update()
    {

    }
}