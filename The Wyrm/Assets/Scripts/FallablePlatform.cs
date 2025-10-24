using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallablePlatform : MonoBehaviour
{
    private GameObject standing;
    private GameObject fallen;

    // Start is called before the first frame update
    void Start()
    {
        standing = transform.GetChild(0).gameObject;
        fallen = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FallOver()
    {
        standing.SetActive(false);
        fallen.SetActive(true);
    }

    public void StandUp()
    {
        standing.SetActive(true);
        fallen.SetActive(false);
    }
}
