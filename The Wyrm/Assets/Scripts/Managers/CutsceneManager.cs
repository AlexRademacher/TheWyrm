using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    private UIManager UI;

    private VideoPlayer startingScene;


    private void OnEnable()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        startingScene = GetComponent<VideoPlayer>(); // connects video
        startingScene.Play(); // starts video
        startingScene.loopPointReached += VideoEnd; // when the video ends run code
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startingScene.clip == null)
        {
            Debug.LogError("Video file missing");
            SkipVideo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // show skip button after pressing escape or space
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the video to end
    /// </summary>
    /// <param name="vp"> The video playing </param>
    private void VideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        if (transform.parent.name.Contains("Intro"))
            UI.CutSceneIntroContinue();
        else
            gameObject.SetActive(false);
    }

    /// <summary>
    /// What the Skip button does on the video
    /// </summary>
    public void SkipButton()
    {
        SkipVideo();
    }

    private void SkipVideo()
    {
        VideoEnd(startingScene); // end video
    }
}
