using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{

    //[SerializeField] Transform goal;
    Vector3 end;
    NavMeshAgent agent;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        end = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        end = player.transform.position;
        agent.destination = end;
        Debug.Log(agent.remainingDistance);
    }
}
