using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndAndChangeScene : MonoBehaviour
{
    public string nextSceneName;

    public GameObject videoImageObject;
    public GameObject blackImageObject;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<VideoPlayer>().loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        videoImageObject.SetActive(false);
        blackImageObject.SetActive(false);

        SceneManager.LoadScene(nextSceneName);
    }

}
