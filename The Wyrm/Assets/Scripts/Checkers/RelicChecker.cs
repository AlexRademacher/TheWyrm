using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicChecker : MonoBehaviour
{
    private GameManager GM;

    private BoxCollider objectCollider;
    private Bounds objectBounds;

    private bool found = false;
    private bool check;
    private int maxChecks = 100;
    private int currChecks = 0;

    [Header("Checker Settings")]
    [Tooltip("The size of the box area being checked"), SerializeField]
    private Vector3 boxSize;
    [Tooltip("The postion of the box on the object"), SerializeField]
    private float maxDistance;
    [Tooltip("Which layer the code is checking for"), SerializeField]
    private LayerMask layerMask;

    [Header("Debugger")]
    [Tooltip("Turns on Relic Check Debugging"), SerializeField]
    private bool relicDebug;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        objectCollider = transform.GetComponent<BoxCollider>();
        objectBounds = objectCollider.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (!found && check && currChecks < maxChecks)
        {
            Collider[] overlapObjects = Physics.OverlapBox(objectBounds.center, objectBounds.extents, Quaternion.identity);

            foreach (Collider overlapObject in overlapObjects)
            {
                if (overlapObject.gameObject.name.Contains("Relic"))
                {
                    check = false;
                    found = true;

                    if (relicDebug)
                        Debug.Log("Relic has been found");

                    GM.RelicFound();
                }
            }

            currChecks++;

            if (relicDebug)
                Debug.Log("Collider overlaps array " + overlapObjects.Length);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            check = true;
            currChecks = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            check = false;
        }
    }
}
