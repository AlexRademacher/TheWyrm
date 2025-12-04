using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private UIManager UI;

    private ParticleSystem particles;
    private SpriteRenderer Renderer;
    private Canvas SpriteCanvas;

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
        Debug.LogWarning("picking up!!!");
        if (particles != null)
        {
            //Debug.Log("PARITCLES :D :D :D :D");
            particles.Play();
        }

        if (!transform.TryGetComponent<SpriteRenderer>(out Renderer)) {
            Debug.LogWarning("killing npc");
            if (transform.GetChild(1).TryGetComponent<Canvas>(out SpriteCanvas))
            {
                Debug.LogWarning("killing npc");
            }
        }
            

        if (Renderer != null)
        {
            Renderer.enabled = false;
        }
            

        if (SpriteCanvas != null)
            SpriteCanvas.enabled = false;

        if (transform.name.Contains("NPC"))
        {
            if (transform.TryGetComponent<BoxCollider>(out BoxCollider collider))
                collider.enabled = false;
            else if (transform.TryGetComponent<MeshCollider>(out MeshCollider collider2))
                collider2.enabled = false;
        }
        else
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Colected object");
        }
            
        //transform.position = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
    }
}
