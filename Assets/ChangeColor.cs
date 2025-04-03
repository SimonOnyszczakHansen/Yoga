using System.Collections;
using System.Threading;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    [SerializeField]
    private AudioSource myAudioSource;

    [SerializeField]
    private AudioClip[] myAudioClips;

    private Color[] chakraColors = new Color[]
    {
        new Color(0.8f, 0f, 0f), // Root Chakra – Red
        new Color(1f, 0.2f, 0f), // Sacral Chakra – Orange
        new Color(1f, 0.85f, 0f), // Solar Plexus Chakra – Yellow
        new Color(0f, 0.8f, 0f), // Heart Chakra – Green
        new Color(0.5f, 0.8f, 0.9f), // Throat Chakra – Light-Blue
        new Color(0.4f, 0f, 0.925f), // Third Eye Chakra – Indigo
        new Color(0.3f, 0f, 0.5f), // Crown Chakra – Purple with a hint of White
    };

    private int currentColorIndex = 0;
    private int currentAudioIndex = 0;

    void Start()
    {
        StartCoroutine(ChakraColorChange());
    }

    void PlayAudio(int index)
    {
        myAudioSource.clip = myAudioClips[index];
        myAudioSource.Play();
    }

    private IEnumerator ChakraColorChange()
    {
        while (true)
        {
            Color startColor = myMaterial.color;
            Color targetColor = chakraColors[currentColorIndex];
            float duration = 2.5f; // Duration of the fade
            float elapsedTime = 0f;

            // Smoothly transition to the next color
            while (elapsedTime < duration)
            {
                myMaterial.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
                myMaterial.SetColor("_BackgroundColor", myMaterial.color);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            myMaterial.color = targetColor;
            myMaterial.SetColor("_BackgroundColor", targetColor);

            PlayAudio(currentAudioIndex);

            Debug.Log(
                "Current Clip: " + currentAudioIndex + " Current Color: " + currentColorIndex
            );

            yield return new WaitForSeconds(myAudioClips[currentAudioIndex].length);

            currentColorIndex = (currentColorIndex + 1) % chakraColors.Length;
            currentAudioIndex = (currentAudioIndex + 1) % myAudioClips.Length;
        }
    }

    void Update() { }
}
