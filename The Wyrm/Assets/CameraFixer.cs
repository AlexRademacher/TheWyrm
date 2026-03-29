using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixer : MonoBehaviour
{
    [SerializeField] Vector3 storedRot = new Vector3 (0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StorePos() 
    {
        storedRot = transform.eulerAngles;
    }

    public void LoadPos()
    {
        transform.eulerAngles = new Vector3(0, storedRot.y, 0);
    }
}
