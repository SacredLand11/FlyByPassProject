using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    CanvasGroup fader;
    void Start()
    {
        fader = GetComponent<CanvasGroup>();
        StartCoroutine(FadeOutIn());
    }

    public IEnumerator FadeOutIn()
    {
        yield return FadeOut(.2f);
        yield return FadeIn(.2f);
    }
    IEnumerator FadeOut(float time)
    {
        while (fader.alpha < 1)
        {
            fader.alpha += Time.deltaTime / time;
            yield return null;
        }
    }
    IEnumerator FadeIn(float time)
    {
        while (fader.alpha > 0)
        {
            fader.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
}
