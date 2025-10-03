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

    // Start is called before the first frame update
    void Start()
    {
        
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
                StartCoroutine(SpawnCountdown(waitTime));
            }
            else if (Random.Range(5, 7) > 4)
            {
                StartCoroutine(SpawnCountdown(waitTime));
            }
        }
        
    }

    private IEnumerator SpawnCountdown(int waitTime)
    {
        spawnedWyrm = true;

        yield return new WaitForSecondsRealtime(waitTime);

        CurrentWyrm = Instantiate(Wyrm, transform);

        if (CurrentWyrm.TryGetComponent<WyrmManager>(out WyrmManager WM))
        {
            if (playerPosition != null)
            {
                WM.SetPlayerPosition(playerPosition);
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
