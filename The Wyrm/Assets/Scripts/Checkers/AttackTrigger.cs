using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private GameObject Attack;


    // Start is called before the first frame update
    void Start()
    {
        for (int c = transform.childCount - 1; c >= 0; c--)
        {
            if (transform.GetChild(c).name.Contains("Claw") || transform.GetChild(c).name.Contains("Swipe") || transform.GetChild(c).name.Contains("Attack"))
            {
                Attack = transform.GetChild(c).gameObject;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Attack.activeSelf)
        {
            Attack.SetActive(true);
        }
    }
}
