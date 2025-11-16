using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyrmSoundManager : MonoBehaviour
{
    private GameManager GM;

    private Transform player;

    private AudioSource Running;
    private AudioSource Yell;
    private AudioSource Bite;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        player = GameObject.Find("Player").GetComponent<Transform>();

        if (!transform.GetChild(1).TryGetComponent<AudioSource>(out Running))
        {
            Debug.LogWarning("Could not find Running sound on Wyrm");
        }

        if (!transform.GetChild(2).TryGetComponent<AudioSource>(out Yell))
        {
            Debug.LogWarning("Could not find Yell sound on Wyrm");
        }

        if (!transform.GetChild(3).TryGetComponent<AudioSource>(out Bite))
        {
            Debug.LogWarning("Could not find Bite sound on Wyrm");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.GetPauseState() || GM.GetDeadState() || GM.GetCreditsState() || GM.GetLoadingState())
        {
            Running.Pause();
            Yell.Pause();
        }
        else
        {
            if (!Running.isPlaying)
                Running.UnPause();

            if (!Yell.isPlaying)
                Yell.UnPause();
        }
    }

    public void BiteSound()
    {
        Bite.time = .5f;
        Bite.Play();
    }
}
