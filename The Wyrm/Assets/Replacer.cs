using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Replacer : MonoBehaviour
{
    [SerializeField] GameObject newBrother;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replace() 
    {
        Debug.Log("Replaced");
        Instantiate(newBrother, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject, 2f);
    }
}
