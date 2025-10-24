using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private UIManager UI;

    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (transform.gameObject.layer == 7)
        {
            if (!transform.GetChild(0).TryGetComponent<ParticleSystem>(out particles))
            {
                Debug.LogWarning("Particles for relic " + transform.name + " not found");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUp()
    {
        if (particles != null)
        {
            //Debug.Log("PARITCLES :D :D :D :D");
            particles.Play();
        }

        transform.GetChild(1).gameObject.SetActive(false);

        if (transform.parent.name.Contains("NPC"))
            transform.GetComponent<MeshCollider>().enabled = false;
        else
            transform.GetComponent<BoxCollider>().enabled = false;
        //transform.position = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
    }
}
