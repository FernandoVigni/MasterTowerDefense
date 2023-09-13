using System.Collections;
using UnityEngine;
using TMPro;

public class CanvaMovementObjet : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        FadeOutAndDeactivate();
    }

    public void SetInitialAlpha()
    {
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 255); ;
    }

    public void FadeOutAndDeactivate()
    {
        StartCoroutine(FadeTextOut());
    }

    private IEnumerator FadeTextOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        SetInitialAlpha();
    }
}
