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

    private readonly Color[] chakraColors = new Color[]
    {
        new Color(0f, 0f, 0f), // Black for intro
        new Color(1f, 0f, 0f), // Root Chakra – Red
        new Color(1f, 0.5f, 0f), // Sacral Chakra – Orange
        new Color(1f, 0.902f, 0f), // Solar Plexus Chakra – Yellow
        new Color(0f, 0.7961f, 0f), // Heart Chakra – Green
        new Color(0.0588f, 0.5020f, 0.9490f), // Throat Chakra – Blue
        new Color(0.4353f, 0f, 0.8706f), // Third Eye Chakra – Purple
        new Color(0.85f, 0.684f, 0.75f)
    };
    private Color chakraGradiant = new Color(0.5f, 0.5f, 0.5f); // Default color for the gradient

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
                myMaterial.SetColor("_Color", myMaterial.color);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            PlayAudio(currentAudioIndex);

            myMaterial.color = targetColor;
            myMaterial.SetColor("_Color", targetColor);

            yield return new WaitForSeconds(myAudioClips[currentAudioIndex].length);

            currentColorIndex = (currentColorIndex + 1) % chakraColors.Length;
            currentAudioIndex = (currentAudioIndex + 1) % myAudioClips.Length;
        }
    }

    void Update() { }
}
