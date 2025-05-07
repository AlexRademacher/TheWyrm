using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveChangerTrigger : MonoBehaviour
{
    GameManager GM;
    CameraManager CM;

    private bool changed = false;

    [SerializeField] GameObject wyrmSpawner;
    private GameObject wyrm;


    [Header("Debugger")]
    [Tooltip("Turns on Relic Check Debugging"), SerializeField]
    private bool debug;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        CM = GameObject.Find("Cameras").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !changed)
        {
            changed = true;

            if (!GM.GetTimerType())
                GM.AddToTimer(60);

            if (CM != null)
            {
                //CM.SetCameraPerspective(true);
                //player.gameObject.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }

            if (wyrmSpawner != null)
            {
                if (wyrmSpawner.TryGetComponent<spawnWyrm>(out spawnWyrm SW))
                {
                    wyrm = SW.Spawn();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            changed = false;

            if (CM != null)
            {
                //CM.SetCameraPerspective(false);
            }

            if (wyrm != null)
            {
                //Destroy(wyrm);
            }
        }
    }
}
