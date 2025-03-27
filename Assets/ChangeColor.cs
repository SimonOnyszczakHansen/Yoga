using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color newBaseColor = Color.red;

    private Renderer cubeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.color = newBaseColor;

        cubeRenderer.material.EnableKeyword("_EMISSION");
        cubeRenderer.material.SetColor("_EmissionColor", newBaseColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
