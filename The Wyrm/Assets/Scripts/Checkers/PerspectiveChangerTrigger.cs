using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveChangerTrigger : MonoBehaviour
{
    CameraManager CM;

    private bool changed = false;

    [SerializeField] GameObject wyrmSpawner;
    private GameObject wyrm;
    

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

            if (CM != null)
            {
                CM.SetCameraPerspective(true);
                other.gameObject.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
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
                CM.SetCameraPerspective(false);
            }

            if (wyrm != null)
            {
                Destroy(wyrm);
            }
        }
    }
}
