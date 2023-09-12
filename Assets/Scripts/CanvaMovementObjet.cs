using System.Collections;
using UnityEngine;
using TMPro;

public class CanvaMovementObjet : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public TextMeshProUGUI textMeshPro;
    private Color startColor;

    private void Start()
    {
        startColor = textMeshPro.color;
        FadeOutAndDeactivate();
    }

    public void SetInitialColor()
    {
        textMeshPro.color = startColor;
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
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        SetInitialColor();
    }
}
