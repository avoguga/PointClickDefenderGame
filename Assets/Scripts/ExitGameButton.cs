using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    // Função para sair do jogo
    public void ExitGame()
    {
        // Fecha o jogo
        Debug.Log("Sair do jogo!");

        // Sair do jogo (não funciona no editor, apenas em builds)
        Application.Quit();
    }
}
