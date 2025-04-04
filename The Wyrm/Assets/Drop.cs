using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Vector3 rotatepoint;
    Vector3 rotateAxis;
    public bool down = false;


    // Start is called before the first frame update
    void Start()
    {
        rotatepoint = new Vector3(this.transform.position.x, this.transform.position.y - 1.3f, this.transform.position.z);
        rotateAxis = new Vector3(this.transform.position.x * this.transform.rotation.y + 1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropThis() 
    {
        Debug.Log("Drop" + this.name);
        this.transform.RotateAround(rotatepoint, transform.localRotation * Vector3.right, 90);
        down = true;
    }

    public void upThis() 
    {
        this.transform.RotateAround(rotatepoint, transform.localRotation * Vector3.right, -90);
        down = false;
    }
}
