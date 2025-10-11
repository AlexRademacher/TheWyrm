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

    private bool inArena;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (SceneManager.GetActiveScene().name.Contains("Arena"))
        {
            player = GameObject.Find("Player").GetComponent<Transform>();


        }

        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Cursor.visible && !GM.GetLoadingState())
        {
            if (inArena)
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
    }

    public void SetPlayerPosition(Transform playerTransform)
    {
        player = playerTransform;
    }

    private void GotoNextPoint()
    {
        
    }

    public IEnumerator StartCountdownToLeave(int time, WyrmSpawnManager WSM)
    {
        yield return new WaitForSecondsRealtime(time);
        WSM.WyrmLeft();
        Destroy(gameObject);
    }
}
