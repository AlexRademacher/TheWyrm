using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveChangerTrigger : MonoBehaviour
{
    private GameManager GM;
    private WyrmSpawnManager WSM;

    private bool entered = false;

    [Tooltip("Where the wyrm is being spawned"), SerializeField] 
    private GameObject WyrmSpawner;
    private GameObject NewWyrm;
    private int wyrmWait = 0;

    private bool respawnWyrm = false;

    [Tooltip("The Wrym will leave sooner if true, if false they will follow a path for a bit"), SerializeField]
    private bool leaveFast;
    [Tooltip("The Wrym will spawn sooner if true"), SerializeField]
    private bool spawnFast;

    [Header("Debugger")]
    [Tooltip("Turns on Relic Check Debugging"), SerializeField]
    private bool debug;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void TryGetWyrm(Transform playerTransform)
    {
        if (WyrmSpawner != null)
        {
            Debug.Log("Player has entered");

            if (WyrmSpawner.TryGetComponent<WyrmSpawnManager>(out WSM))
            {
                wyrmWait = Random.Range(1, 10);
                WSM.TrySpawnWyrm(playerTransform, wyrmWait, respawnWyrm);

                StartCoroutine(GetWyrm());
            }
            else
            {
                Debug.LogWarning("Could not find the Wyrm Spawn Manager script");
            }
        }
    }

    private IEnumerator GetWyrm()
    {
        Debug.Log("Time to wait " + wyrmWait);
        yield return new WaitForSecondsRealtime(wyrmWait);

        if (WSM != null)
        {
            NewWyrm = WSM.GetCurrentWyrm();
        }
        else
            Debug.LogWarning("Could not find the Wyrm Spawn Manager script");
    }

    private IEnumerator RemovalCheck()
    {
        if (spawnFast)
        {
            wyrmWait = wyrmWait / 2;
        }
        yield return new WaitForSecondsRealtime(wyrmWait);

        if (NewWyrm != null && !entered)
        {
            if (NewWyrm.TryGetComponent<WyrmManager>(out WyrmManager WM))
            {
                respawnWyrm = true;
                if (leaveFast)
                    StartCoroutine(WM.StartCountdownToLeave(Random.Range(1, 3), WSM));
                else
                    StartCoroutine(WM.StartCountdownToLeave(Random.Range(3, 10), WSM));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !entered)
        {
            if (!GM.GetTimerType())
                GM.AddToTimer(60);

            Debug.Log("Player entering");
            if (!entered)
                TryGetWyrm(other.transform);

            entered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            entered = false;

            if (NewWyrm != null)
            {
                if (NewWyrm.TryGetComponent<WyrmManager>(out WyrmManager WM))
                {
                    respawnWyrm = true;
                    if (leaveFast)
                        StartCoroutine(WM.StartCountdownToLeave(Random.Range(3, 6), WSM));
                    else
                        StartCoroutine(WM.StartCountdownToLeave(Random.Range(10, 30), WSM));
                }
            }
            else
            {
                StartCoroutine(RemovalCheck());
            }
        }
    }
}
