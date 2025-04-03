using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{

    [SerializeField] Transform goal;
    Vector3 end;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        end = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        end = goal.position;
        agent.destination = end;
        Debug.Log(agent.remainingDistance);
    }
}
