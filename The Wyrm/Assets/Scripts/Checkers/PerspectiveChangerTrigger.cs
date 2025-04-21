using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveChangerTrigger : MonoBehaviour
{
    CameraManager CM;

    private bool changed = false;

    [SerializeField] GameObject wyrmSpawner;
    

    // Start is called before the first frame update
    void Start()
    {
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
            CM.SetCameraPerspective(!CM.GetCameraPerspective());
            wyrmSpawner.GetComponent<spawnWyrm>().spawn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            changed = false;
        }
    }
}
