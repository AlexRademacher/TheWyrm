using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Transform objectTransform;

    [Header("Checker Settings")]
    [Tooltip("The size of the box area being checked"), SerializeField]
    private Vector3 boxSize;
    [Tooltip("The postion of the box on the object"), Range(-4, 4), SerializeField]
    private float maxDistance;
    [Tooltip("Which layer the code is checking for"), SerializeField]
    private LayerMask layerMask;

    [Header("Debugger")]
    [Tooltip("Turns on Debugging"), SerializeField]
    private bool debug;

    /// <summary>
    /// Checks if an object is touching the ground. Forms a box under the object and check if a layer enters it.
    /// </summary>
    /// <param name="newObjectTransform"> The Transform of the object </param>
    /// <returns> Returns True if touching the ground </returns>
    public bool GroundCheck(Transform newObjectTransform)
    {
        if (newObjectTransform != null)
        {
            objectTransform = newObjectTransform; // saves the transform for the debug

            // Checks within a box like area under the object in use if the layer selected enters it
            // returns true if the layer is within the box
            // returns false if not
            if (Physics.BoxCast(objectTransform.position, boxSize, -objectTransform.up, objectTransform.rotation, maxDistance, layerMask))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    /// <summary>
    /// Draws the area being checked
    /// </summary>
    private void OnDrawGizmos()
    {
        // Shows when debug is turned on
        if (debug && objectTransform != null)
        {
            Gizmos.color = Color.red; // pick color
            Gizmos.DrawCube(objectTransform.position - objectTransform.up * maxDistance, boxSize); // Draw box
        }
        else
            return;
    }
}