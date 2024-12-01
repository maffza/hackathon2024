using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ScreenBlinkEffect : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;
    private bool isBlinking = false;

    void Start()
    {
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0f;
        }
    }

    public void TriggerBlink()
    {
        if (!isBlinking)
        {
            StartCoroutine(BlinkEffect());
        }
    }

    private IEnumerator BlinkEffect()
    {
        isBlinking = true;

        vignette.intensity.value = 0.5f;
        yield return new WaitForSeconds(0.5f);

        vignette.intensity.value = 0f;
        isBlinking = false;
    }
}