using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;  // Przestrzeń nazw dla Post-Processing w URP
using System.Collections;

public class ScreenBlinkEffect : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;
    private bool isBlinking = false;

    public float duration = 2f;

    void Start()
    {
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0f;
        }
    }

    public void CloseEye()
    {
        if (!isBlinking)
        {
            StartCoroutine(LerpVignette(1f));
        }
    }

    public void OpenEye()
    {
        if (!isBlinking)
        {
            StartCoroutine(LerpVignette(0f));
        }
    }

    private IEnumerator LerpVignette(float targetIntensity)
    {
        isBlinking = true;

        float startIntensity = vignette.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.intensity.value = targetIntensity;
        isBlinking = false;
    }
}