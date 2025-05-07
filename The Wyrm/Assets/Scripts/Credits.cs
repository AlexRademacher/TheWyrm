using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private bool startCredits = true;
    private bool start = false;
    private bool waitTime = false;

    [SerializeField]
    private GameObject escapeText;
    private bool escape = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find("Background Music").transform.GetChild(0).TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audio.Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startCredits)
        {
            if (!start)
            {
                StartCoroutine(StartScroll());
            }

            if (start && !waitTime)
            {
                StartCoroutine(Scrolling());

                if (Input.anyKey)
                {
                    escapeText.SetActive(true);
                    escape = true;
                }

                if (escape && Input.GetKey(KeyCode.Escape))
                {
                    if (GameObject.Find("Game Manager").TryGetComponent<GameManager>(out GameManager GM))
                    {
                        GM.CloseGame();
                    }
                }
            }
        }
    }

    private IEnumerator StartScroll()
    {
        yield return new WaitForSeconds(3f);
        start = true;
    }

    private IEnumerator Scrolling()
    {
        waitTime = true;
        yield return new WaitForSeconds(.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        waitTime = false;

        if (transform.position.y > 2500)
        {
            startCredits = false;
            StartCoroutine(End());
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(3f);
        if (GameObject.Find("Game Manager").TryGetComponent<GameManager>(out GameManager GM))
        {
            GM.CloseGame();
        }
    }
}
