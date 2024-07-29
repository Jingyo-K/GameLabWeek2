using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    //FadeOut
    public Image[] image;
    void Start()
    {
        GameEvents.onPlayerTakeDamage += FadeOut;
        image = GetComponentsInChildren<Image>();
    }

    void FadeOut(GameEvents gameEvents)
    {
        int i;
        for(i=0; i<image.Length;i++)
        {
            image[i].color = Color.red;
            StartCoroutine(FadeOut(image[i]));
        }
    }
    IEnumerator FadeOut(Image _fadeImage)
    {
        Color curColor = _fadeImage.color;
        float _fadeTime = 1.0f;
        Color targetColor = new Color(0, 0, 0, 0);
        float time = 0;

        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(curColor, targetColor, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        _fadeImage.color = targetColor;
        yield break;
    }
    private void OnDestroy()
    {
        GameEvents.onPlayerTakeDamage -= FadeOut;
    }
}
