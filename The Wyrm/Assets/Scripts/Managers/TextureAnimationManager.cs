using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimationManager : MonoBehaviour
{
    [SerializeField]
    private List<Texture2D> Textures;

    private MeshRenderer TargetRenderer;

    [SerializeField, Range(0, 24)]
    private int Speed;
    private float currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<MeshRenderer>(out TargetRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        currentIndex += Speed * Time.deltaTime;

        var i = (int)currentIndex;

        if (i > Textures.Count - 1)
        {
            currentIndex = 0;
            i = 0;
        }

        // _BaseMap for URP Lit  _MainTex for built in RP
        if (TargetRenderer != null)
        {
            TargetRenderer.material.SetTexture("_MainTex", Textures[i]);
            Debug.Log("Renderering animation");
        }
        else
            Debug.LogWarning("Renderer not found");
    }
}
