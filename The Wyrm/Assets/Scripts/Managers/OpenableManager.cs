using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableManager : MonoBehaviour
{
    private GameObject Closed;
    private GameObject Open;
    private AudioSource AS;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponentInParent<AudioSource>();
        Closed = transform.GetChild(0).gameObject;
        Open = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenControl()
    {
        if (Closed.activeSelf)
        {
            OpenUp();
        }
        else if (Open.activeSelf)
        {
            CloseDown();
        }
    }

    private void OpenUp()
    {
        if (AS != null)
        {
            AS.pitch = Random.Range(0.9f, 1.1f);
            AS.PlayOneShot(openSound);
        }
        
        Closed.SetActive(false);
        Open.SetActive(true);
    }

    private void CloseDown()
    {
        if (AS != null)
        {
            AS.pitch = Random.Range(0.9f, 1.1f);
            AS.PlayOneShot(closeSound);
        }
        
        Closed.SetActive(true);
        Open.SetActive(false);
    }
}
