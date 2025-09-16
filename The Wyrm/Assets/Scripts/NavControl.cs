using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] Transform goal;
    Vector3 end;
    NavMeshAgent agent;

    NavMeshPath path;

    bool checkingHide = false;
    [SerializeField] bool inArena;

    [SerializeField] private Transform[] points;
    private int destPoint = 0;
    [SerializeField] int wyrmNumber;



    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        goal = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        end = agent.destination;
        agent.speed = 3.5f;
        path = new NavMeshPath();

        if (!inArena) 
        {
            //Find the nav points
            
            points[0] = GameObject.Find("PatrolPoint" + (wyrmNumber + 1)).transform;
            points[1] = GameObject.Find("PatrolPoint" + (wyrmNumber + 2)).transform;
            points[2] = GameObject.Find("PatrolPoint" + (wyrmNumber + 3)).transform;


            GotoNextPoint();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!Cursor.visible && !GM.GetLoadingState()) { 
            end = goal.position;
            
            if (agent.CalculatePath(end, path) && path.status == NavMeshPathStatus.PathComplete) 
            {
                agent.destination = end;
                //Start coroutine.  Wait time Period (3 seconds???).  If target is still hiding return to given point or despawn.
                //Check scene as well using bool maybe 
                //These notes are for later when i implement what the wyrm should do after the player hides for a while
            }
            else
            {
                if (!inArena)
                    if (agent.remainingDistance < 0.5f)
                    {
                        GotoNextPoint();
                    }
                /*agent.destination = agent.transform.position;
                if (!checkingHide) 
                {
                    if(!inArena)
                        StartCoroutine(checkHide());
                    checkingHide = true;
                }*/
            }
            /*if (agent.remainingDistance < 0.5f && !seePlayer)
            {
                GotoNextPoint();
            }*/
            //Debug.Log(agent.remainingDistance);
            if (agent.isOnOffMeshLink)
            { 
                agent.speed = 6.5f / 6f; //Change the second number to slow down or speed up the wyrm
                //Play an animation for jumping over something here
            }
            else
            {
                agent.speed = 6.5f;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("trigger");
        if (other.tag == "Slow")
        {
            agent.speed = 7f * 0.5f;
            Debug.Log("slow zone");
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("AHHHHH");
            GameObject.Find("Player").GetComponent<Player>().PlayerKilled();
            /*
             if (TryGetComponent<Player>(out Player P))
            {
                Debug.Log("PlayerFound");
                P.PlayerKilled();
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Slow")
        {
            agent.speed = 7f;
        }
    }
    
    IEnumerator checkHide() 
    {
        yield return new WaitForSeconds(3);
        if (!(agent.CalculatePath(end, path) && path.status == NavMeshPathStatus.PathComplete))
        {
            Destroy(this.gameObject);
        }
        else 
        {
            checkingHide = false;
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;


        // Set the agent to go to the currently selected destination.

        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void giveNumber(int wyrmNumberPassed) 
    {
        wyrmNumber = wyrmNumberPassed;
    }
}