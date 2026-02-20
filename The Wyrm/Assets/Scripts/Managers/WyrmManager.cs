using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class WyrmManager : MonoBehaviour
{
    private GameManager GM;

    private Transform player;

    private WyrmSoundManager WSM;

    private NavMeshAgent agent;
    private NavMeshPath path;

    private Transform[] points;
    private int currentPoint = 0;

    [Tooltip("The Wrym number for which type"), SerializeField, Range(1,3)]
    private int wyrmNum;

    [Tooltip("The Wrym number for which type"), SerializeField]
    private Sprite[] wyrmSprites;

    private bool inArena = false;
    private bool playerHiding = false;
    private bool pointsReset = false;
    private bool letingPlayerGo = false;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        player = GameObject.Find("Player").GetComponent<Transform>();

        if (!transform.TryGetComponent<WyrmSoundManager>(out WSM))
        {
            Debug.LogWarning("Could not find sound manager on Wyrm");
        }

        if (SceneManager.GetActiveScene().name.Contains("Arena"))
        {
            inArena = true;

            if (points == null && transform.parent.childCount > 1)
            {
                points = new Transform[transform.parent.childCount - 1];

                for (int i = 1; i < transform.parent.childCount - 1; i++)
                {
                    if (transform.parent.GetChild(i).name.Contains("point"))
                    {
                        points[i - 1] = transform.parent.GetChild(i);
                    }
                }

                while (points[currentPoint] == null)
                {
                    currentPoint++;
                }
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            int num = Random.Range(1, 3);
            if (num == 1)
            {
                wyrmNum = 1;
            }
            else
            {
                wyrmNum = 3;
            }
        }

        if (transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer wyrmLook))
        {
            wyrmLook.sprite = wyrmSprites[wyrmNum - 1];
        }
        else
            Debug.Log("Default wyrm sprite was placed as a default as the correct sprite failed to be applied");

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
                    WyrmArenaMovement();
                }
                else
                {
                    WyrmVillageMovement();
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
            Debug.LogWarning("Thjere are no points stored in the wyrm");
            return;
        }

        //Debug.Log("The closet point is " + currentPoint + " and is being set now");
            
        if (points[currentPoint] != null)
            agent.destination = points[currentPoint].position;

        currentPoint = (currentPoint + 1) % points.Length;
        //Debug.Log("Going to point " + currentPoint);
    }

    private void ResetPoints()
    {
        if (points[currentPoint] != null)
        {
            float closestPointDistance = Vector3.Distance(points[currentPoint].transform.position, transform.position);
            int num = 0;

            foreach (Transform point in points)
            {
                if (point != null)
                {
                    float pointDistance = Vector3.Distance(point.transform.position, transform.position);

                    if (closestPointDistance > pointDistance)
                    {
                        closestPointDistance = pointDistance;
                        currentPoint = num;
                    }

                    num++;
                }
            }

            pointsReset = true;
            //Debug.Log("pointsReset to true!!!!!!!");
            //Debug.Log(closestPointDistance);
            //Debug.Log("Wyrm: " + transform.name + " has the closest point " + currentPoint);
        }
    }

    private void WyrmVillageMovement()
    {
        if (!playerHiding && agent.CalculatePath(player.position, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            if (player.TryGetComponent<Player>(out Player playerScript))
            {
                if (!playerScript.CheckIfHiding())
                {
                    agent.destination = player.position;

                    agent.speed = 8;
                    pointsReset = false;
                }
                else
                {
                    playerHiding = true;
                }
            }
        }
        else
        {
            if (!pointsReset)
                ResetPoints();
            else
                FollowPoints();
        }
    }

    private void WyrmArenaMovement()
    {
        if (!playerHiding && agent.CalculatePath(player.position, path))
        {
            if (player.TryGetComponent<Player>(out Player playerScript))
            {
                if (!playerScript.CheckIfHiding())
                {
                    agent.destination = player.position;

                    if (wyrmNum == 1)
                        Wyrm1Movement();
                    else if (wyrmNum == 2)
                        Wyrm2Movement();
                    else if (wyrmNum == 3)
                        Wyrm3Movement();

                    //Debug.LogWarning("Speed: " + agent.speed);
                    pointsReset = false;
                }
                else
                {
                    playerHiding = true;

                    if (!pointsReset)
                    {
                        //Debug.Log("Points Reset Before: " + pointsReset);
                        ResetPoints();
                        //Debug.Log("Points Reset after: " + pointsReset);
                        letingPlayerGo = true;
                    }
                    else
                    {
                        FollowPoints();
                        //GotoNextPoint();
                    }
                }
            }
            else
            {
                Debug.LogError("Player script on player not found for wyrm");
            }
        }
        else
        {
            if (!pointsReset)
            {
                //Debug.Log("Points Reset Before2: " + pointsReset);
                ResetPoints();
                //Debug.Log("Points Reset after2: " + pointsReset);
            }
            else
            {
                FollowPoints();
            }
            //Debug.Log("The closet point is " + currentPoint);

        }
    }
    
    private void Wyrm1Movement()
    {
        agent.speed = 8;
    }

    private void Wyrm2Movement()
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

    private void Wyrm3Movement()
    {
        if (agent.remainingDistance < 1000)
            agent.speed = 1000 - agent.remainingDistance;
        else if (agent.remainingDistance > 1000)
            agent.speed = 1000;
    }

    private void FollowPoints()
    {
        if (agent.remainingDistance < 0.5f || (letingPlayerGo && playerHiding))
        {
            if (letingPlayerGo)
                letingPlayerGo = false;

            if (agent.speed != 6)
            {
                agent.speed = 6;
            }

            GotoNextPoint();
        }

        if (agent.CalculatePath(player.position, path))
        {
            playerHiding = false;
        }
    }

    public IEnumerator StartCountdownToLeave(int time, WyrmSpawnManager WSM)
    {
        Debug.Log("Wyrm will leave in " + time + " seconds");
        yield return new WaitForSecondsRealtime(time);
        WSM.WyrmLeft();

        if (gameObject != null && gameObject.activeSelf)
            Destroy(gameObject);
    }

    /*private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Debug.Log("Got child"); 

            if (WSM != null)
                WSM.BiteSound();

            if (!playerHiding && player.TryGetComponent<Player>(out Player playerScript))
                playerScript.PlayerKilled();
        }
    }*/
}
