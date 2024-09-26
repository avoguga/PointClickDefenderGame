using UnityEngine;
using UnityEngine.UI;

public class FadeInOnStart : MonoBehaviour
{
    public Image fadeImage; // A imagem preta para o fade
    public float fadeDuration = 2f; // Duração do fade in
    private float fadeTime = 0f;
    private bool isFadingIn = true;

    void Start()
    {
        // Certifique-se de que a imagem do fade começa totalmente preta
        Color newColor = fadeImage.color;
        newColor.a = 1; // Totalmente opaco
        fadeImage.color = newColor;
        fadeImage.gameObject.SetActive(true); // A imagem começa ativa
    }

    void Update()
    {
        // Se o fade in estiver ativado, execute o fade
        if (isFadingIn)
        {
            fadeTime += Time.deltaTime;
            float alphaValue = 1 - Mathf.Clamp01(fadeTime / fadeDuration); // Calcula o alpha com base no tempo decrescente

            // Atualiza a opacidade (alpha) da imagem
            Color newColor = fadeImage.color;
            newColor.a = alphaValue;
            fadeImage.color = newColor;

            // Quando o fade in estiver completo, desativa a imagem de fade
            if (alphaValue <= 0)
            {
                fadeImage.gameObject.SetActive(false); // Fade in completo, imagem desaparece
                isFadingIn = false;
            }
        }
    }
}
