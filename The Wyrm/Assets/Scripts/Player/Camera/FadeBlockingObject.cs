using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlockingObject : MonoBehaviour
{
    private LayerMask layerMask;
    private Transform player;
    private Camera camera3rdPerson;

    [Header("Object Fade Adjustments")]
    [Tooltip("How much we fade the Alpha each frame"), SerializeField]
    private float FadedAlpha = 0.33f;
    [Tooltip("How many times we check for an object in front of the camera per second"), SerializeField]
    private float ChecksPerSecond = 10.0f;
    [Tooltip("How many times we fade per frame"), SerializeField]
    private int FadeFPS = 30;

    private List<ObjectFade> ObjectsBlockingView = new List<ObjectFade>();
    private List<int> IndexesToClear = new List<int>();
    private Dictionary<ObjectFade, Coroutine> RunningCoroutines = new Dictionary<ObjectFade, Coroutine>();

    private RaycastHit[] RayHits = new RaycastHit[10];


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CheckForObject()
    {
        WaitForSeconds wait = new WaitForSeconds(1f / ChecksPerSecond);

        while (true)
        {
            if (Physics.RaycastNonAlloc(camera3rdPerson.transform.position, (player.transform.position - camera3rdPerson.transform.position).normalized, RayHits, Vector3.Distance(camera3rdPerson.transform.position, player.transform.position), layerMask) > 0)
            {
                
            }
        }
    }
}
