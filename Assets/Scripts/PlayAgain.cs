using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    public Image fadeImage; // A imagem preta para o fade
    public float fadeDuration = 1f; // Duração do fade
    public AudioClip clickSound; // Som de clique
    private AudioSource audioSource; // Componente para tocar som
    private bool isFadingOut = false;
    private float fadeTime = 0f;

    void Start()
    {
        // Certifique-se de que a imagem do fade começa invisível
        Color newColor = fadeImage.color;
        newColor.a = 0;
        fadeImage.color = newColor;
        fadeImage.gameObject.SetActive(false); // A imagem começa desativada

        // Configura o AudioSource para tocar o som de clique
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Se o fade out estiver ativado, execute o fade
        if (isFadingOut)
        {
            fadeTime += Time.deltaTime;
            float alphaValue = Mathf.Clamp01(fadeTime / fadeDuration); // Calcula o valor de alpha com base no tempo

            // Atualiza a opacidade (alpha) da imagem
            Color newColor = fadeImage.color;
            newColor.a = alphaValue;
            fadeImage.color = newColor;

            // Quando o fade estiver completo, carregue a próxima cena
            if (alphaValue >= 1)
            {
                SceneManager.LoadScene("Level");
            }
        }
    }

    public void PlayAgain()
    {
        // Toca o som de clique quando o botão for pressionado
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        fadeImage.gameObject.SetActive(true); // Ativa a imagem para o fade out
        isFadingOut = true; // Inicia o fade out
    }
}
