using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{

    [SerializeField] Transform goal;
    Vector3 end;
    NavMeshAgent agent;

    NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        end = agent.destination;
        agent.speed = 3.5f;
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        end = goal.position;
        //
        if (agent.CalculatePath(end, path) && path.status == NavMeshPathStatus.PathComplete) //Look into why it cant traverse links anymore
        {
            agent.destination = end;
            //agent.SetPath(path);
        }
        else
        {
            agent.destination = agent.transform.position;
        }
        //Debug.Log(agent.remainingDistance);
        if (agent.isOnOffMeshLink)
        {
            agent.speed = 6.5f / 2f;
        }
        else
        {
            agent.speed = 6.5f;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger");
        if (other.tag == "Slow")
        {
            agent.speed = 7f * 0.5f;
            Debug.Log("slow zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Slow")
        {
            agent.speed = 7f;
        }
    }
}