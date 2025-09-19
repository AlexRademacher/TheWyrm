using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform MyCameraTransform;
	private Transform MyTransform;
	public bool alignNotLook = true;


    void LateUpdate()
    {
		
        var target = MyCameraTransform.position;
        target.y = transform.position.y;
		
        transform.LookAt(target);
    }
    // Use this for initialization
    /*void Start () {
		MyTransform = this.transform;
		MyCameraTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (alignNotLook)
			MyTransform.forward = MyCameraTransform.forward;
		else
			MyTransform.LookAt (MyCameraTransform, Vector3.up);
	}*/
}
