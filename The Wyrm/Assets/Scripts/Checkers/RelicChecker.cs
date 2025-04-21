using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicChecker : MonoBehaviour
{
    GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Relic"))
        {
            gM = GameObject.Find("Game Manager").GetComponent<GameManager>();

            if (gM != null)
            {
                gM.RelicFound();
            }

            Debug.Log("THE RELIC HAS BEEN FOUND");
        }
    }
}
