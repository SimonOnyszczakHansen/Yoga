using System.Collections;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    private Color[] chakraColor = new Color[]
    {
        new Color(0.8f, 0f, 0f), // Root Chakra – Deep Red
        new Color(1f, 0.2f, 0f), // Sacral Chakra – Vivid Orange
        new Color(1f, 0.85f, 0f), // Solar Plexus Chakra – Golden Yellow
        new Color(0f, 0.8f, 0f), // Heart Chakra – Rich Green
        new Color(0f, 0.8f, 0.8f), // Throat Chakra – Pure Turquoise
        new Color(0f, 0f, 1f), // Third Eye Chakra – Deep Indigo
        new Color(0.2f, 0f, 0.4f) // Crown Chakra – Soft Violet

    };

    private int currentColorIndex = 0;
    private float colorChangeInterval = 2f;
    void Start()
    {
        StartCoroutine(ChakraColorChange());
    }

    IEnumerator ChakraColorChange()
    {
        while (true)
        {
            myMaterial.color = chakraColor[currentColorIndex];
            myMaterial.EnableKeyword("_EMISSION");
            myMaterial.SetColor("_EmissionColor", chakraColor[currentColorIndex]);
            currentColorIndex = (currentColorIndex + 1) % chakraColor.Length;
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }

    void Update()
    {

    }
}
