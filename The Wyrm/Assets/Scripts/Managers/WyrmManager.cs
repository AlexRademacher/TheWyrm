using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class WyrmManager : MonoBehaviour
{
    private GameManager GM;

    private Transform player;

    private NavMeshAgent agent;
    private NavMeshPath path;

    private Transform[] points;
    private int currentPoint;

    [Tooltip("The Wrym number for which type"), SerializeField, Range(1,3)]
    private int wyrmNum;

    private bool inArena;
    

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        player = GameObject.Find("Player").GetComponent<Transform>();

        if (SceneManager.GetActiveScene().name.Contains("Arena"))
        {
            inArena = true;

            if (points == null && transform.parent.childCount > 1)
            {
                points = new Transform[transform.parent.childCount - 1];

                for (int i = 1; i < transform.parent.childCount - 1; i++)
                {
                    points[i - 1] = transform.parent.GetChild(i);
                }
            }
        }

        agent = GetComponent<NavMeshAgent>();
        agent.speed = 6;

        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Cursor.visible && !GM.GetLoadingState() && agent != null)
        {
            if (agent.isOnNavMesh)
            {
                if (inArena)
                {
                    if (agent.hasPath && agent.CalculatePath(player.position, path) && path.status == NavMeshPathStatus.PathComplete)
                    {
                        agent.destination = player.position;

                        if (wyrmNum == 1)
                            agent.speed = 8;
                        else if (wyrmNum == 2)
                        {
                            if (agent.remainingDistance < 5)
                                agent.speed = agent.remainingDistance;
                            else if (agent.remainingDistance > 5 && agent.remainingDistance < 15)
                                agent.speed = agent.remainingDistance / 1.5f;
                            else if (agent.remainingDistance > 15 && agent.remainingDistance < 200)
                                agent.speed = agent.remainingDistance / 2;
                            else if (agent.remainingDistance > 200)
                                agent.speed = 100;
                        }
                        else if (wyrmNum == 3)
                            if (agent.remainingDistance < 1000)
                                agent.speed = 1000 - agent.remainingDistance;
                            else if (agent.remainingDistance > 1000)
                                agent.speed = 1000;

                        //Debug.LogWarning("Speed: " + agent.speed);
                    }
                    else
                    {
                        if (agent.remainingDistance < 0.5f)
                        {
                            if (agent.speed != 6)
                            {
                                agent.speed = 6;
                            }

                            GotoNextPoint();
                        }
                    }
                }
                else
                {
                    if (agent.CalculatePath(player.position, path) && path.status == NavMeshPathStatus.PathComplete)
                    {
                        agent.destination = player.position;

                        agent.speed = 8;
                    }
                    else
                    {
                        if (agent.remainingDistance < 0.5f)
                        {
                            if (agent.speed != 6)
                            {
                                agent.speed = 6;
                            }

                            GotoNextPoint();
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Nav Mesh is missing for the Wyrm");
                agent = null;
            }
        }
    }

    public void SetPlayerPosition(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SetNavPoints(Transform[] WyrmNavPoints)
    {
        points = WyrmNavPoints;

        for (int i = 0; i > WyrmNavPoints.Length; i++)
        {
            points[i] = WyrmNavPoints[i];
        }

        if (points != null)
        {
            //Debug.LogWarning("We got the points!");
        }
        else
        {
            Debug.LogWarning("We dont got the points!");
        }
        
    }

    private void GotoNextPoint()
    {
        if (points == null)
        {
            Debug.LogError("Nav mesh points are missing for Wyrm");
            return;
        }

        if (points.Length <= 0)
        {
            Debug.LogWarning("THere are no points stored in the wyrm");
            return;
        }
            
        if (points[currentPoint] != null)
            agent.destination = points[currentPoint].position;

        currentPoint = (currentPoint + 1) % points.Length;
        //Debug.Log("Going to point " + currentPoint);
    }

    public IEnumerator StartCountdownToLeave(int time, WyrmSpawnManager WSM)
    {
        yield return new WaitForSecondsRealtime(time);
        WSM.WyrmLeft();
        Destroy(gameObject);
    }
}
