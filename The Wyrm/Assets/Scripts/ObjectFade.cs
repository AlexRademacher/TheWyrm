using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectFade : MonoBehaviour
{
    private List<Renderer> renderers = new List<Renderer>();
    private Vector3 postion;
    private List<Material> materials = new List<Material>();
    private float initialAlpha;

    
    void Awake()
    {
        postion = transform.position;

        //-----
        // can remove?
        if (renderers.Count == 0)
        {
            renderers.AddRange(gameObject.GetComponentsInChildren<Renderer>());
        }

        for (int i = 0; i < renderers.Count; i++)
        {
            materials.AddRange(renderers[i].materials);
        }
        //----
        initialAlpha = materials[0].color.a;
    }

    //----
    public bool Equals(ObjectFade other)
    {
        return postion.Equals(other.postion);
    }
    //----

    public override int GetHashCode()
    {
        return postion.GetHashCode();
    }
}
