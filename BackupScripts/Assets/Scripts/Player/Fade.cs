using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    Renderer rend;
    bool fade;
    public float speedToFade = 5f;
    [Range(0f,0.9f)]
    public float percentFaded = 0.3f;

    void Start()
    {
        rend = transform.GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CameraCube"))
        {
            fade = true;
            StartCoroutine(FadeOut());
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("CameraCube"))
        {
            fade = false;
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeOut()
    {
        float fadePercent = rend.material.color.a;
        float fadeSpeed = speedToFade;
        Color tempColor = rend.material.color;
        while (fadePercent >= percentFaded)
        {
            if (fade == false)
            {
                yield break;
            }

            fadePercent -= (0.1f * Time.deltaTime * fadeSpeed);
            tempColor.a = fadePercent;
            rend.material.color = tempColor;

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float fadePercent = rend.material.color.a;
        float fadeSpeed = speedToFade;
        Color tempColor = rend.material.color;
        while (fadePercent < 1f)
        {
            if (fade == true)
            {
                yield break;
            }

            fadePercent += (0.1f * Time.deltaTime * fadeSpeed);
            tempColor.a = fadePercent;
            rend.material.color = tempColor;

            yield return null;
        }
    }
}


