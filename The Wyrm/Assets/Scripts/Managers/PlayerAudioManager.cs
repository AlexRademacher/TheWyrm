using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip footstepAudioSource;
    [SerializeField] private AudioClip roar;
    [SerializeField] private AudioClip popUp;
    [SerializeField] private AudioClip mapOpen;
    [SerializeField] private AudioClip mapClose;
    [SerializeField] private AudioClip relicPlace;
    [SerializeField] private AudioClip relicPickup;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip lowTime;

    private float BaseVol;

    private AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        BaseVol = AS.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void fixPitch()
    {
        AS.pitch = 1f;
    }
    private void fixVol()
    {
        AS.volume = BaseVol;
    }

    public void playFootstep()
    {
        AS.pitch = Random.Range(0.8f, 1.2f);
        AS.PlayOneShot(footstepAudioSource);
    }
    public void playRoar()
    {
        fixPitch();
        AS.PlayOneShot(roar);
    }
    public void playPopUp()
    {
        fixPitch();
        AS.PlayOneShot(popUp);
    }
    public void playMapOpen()
    {
        AS.volume = BaseVol * 2f;
        fixPitch();
        AS.PlayOneShot(mapOpen);
        fixVol();
    }
    public void playMapClose()
    {
        AS.volume = BaseVol * 2f;
        fixPitch();
        AS.PlayOneShot(mapClose);
        fixVol();
    }
    public void playRelicPlace()
    {
        fixPitch();
        AS.PlayOneShot(relicPlace);
    }
    public void playRelicPickup()
    {
        fixPitch();
        AS.PlayOneShot(relicPickup);
    }
    public void playJump()
    {
        AS.pitch = Random.Range(0.8f, 1.2f);
        AS.PlayOneShot(jump);
    }
    public void playLowTime()
    {
        fixPitch();
        AS.PlayOneShot(lowTime);
    }
}
