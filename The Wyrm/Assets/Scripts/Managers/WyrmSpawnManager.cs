using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyrmSpawnManager : MonoBehaviour
{
    private Transform playerPosition;

    [Tooltip("The Wrym being spawned"), SerializeField] 
    private GameObject Wyrm;
    private GameObject CurrentWyrm;

    private bool spawnedWyrm = false;

    private Transform[] WyrmPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (WyrmPoints == null && transform.childCount > 0)
        {
            WyrmPoints = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                WyrmPoints[i] = transform.GetChild(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetCurrentWyrm()
    {
        return CurrentWyrm;
    }

    public void TrySpawnWyrm(Transform playerTransform, int waitTime, bool respawning)
    {
        playerPosition = playerTransform;
        Spawn(waitTime, respawning);
    }

    private void Spawn(int waitTime, bool respawning)
    {
        if (!spawnedWyrm)
        {
            if (respawning)
            {
                Debug.Log("spawning wyrm");
                StartCoroutine(SpawnCountdown(1));
            }
            else if (Random.Range(1, 7) > 4)
            {
                Debug.Log("Wyrm spawning");
                StartCoroutine(SpawnCountdown(waitTime));
            }
        }
        
    }

    private IEnumerator SpawnCountdown(int waitTime)
    {
        spawnedWyrm = true;

        yield return new WaitForSecondsRealtime(waitTime);

        CurrentWyrm = Instantiate(Wyrm, transform.position, transform.rotation);

        if (CurrentWyrm.TryGetComponent<WyrmManager>(out WyrmManager WM))
        {
            
            if (playerPosition != null)
            {
                WM.SetPlayerPosition(playerPosition);

                if (WyrmPoints != null)
                {
                    WM.SetNavPoints(WyrmPoints);
                }
            }
        }
        else
        {
            Debug.LogWarning("Wyrm Spawn Manager could not find Wyrm Manager on the spawned Wyrm");
        }
    }

    public void WyrmLeft()
    {
        spawnedWyrm = false;
    }
}
