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

    private bool inArena;
    

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        player = GameObject.Find("Player").GetComponent<Transform>();

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
                    }
                    else
                    {
                        if (agent.remainingDistance < 0.5f)
                        {
                            GotoNextPoint();
                        }
                    }
                }
                else
                {
                    if (agent.CalculatePath(player.position, path) && path.status == NavMeshPathStatus.PathComplete)
                    {
                        agent.destination = player.position;
                    }
                    else
                    {
                        if (agent.remainingDistance < 0.5f)
                        {
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
            Debug.LogWarning("We got the points!");
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
            

        agent.destination = points[currentPoint].position;

        currentPoint = (currentPoint + 1) % points.Length;
        Debug.Log("Going to point " + currentPoint);
    }

    public IEnumerator StartCountdownToLeave(int time, WyrmSpawnManager WSM)
    {
        yield return new WaitForSecondsRealtime(time);
        WSM.WyrmLeft();
        Destroy(gameObject);
    }
}
