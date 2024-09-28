using UnityEngine;

public class MouseCursorManager : MonoBehaviour
{
    public Texture2D customCursor; // Arraste a imagem do cursor para este campo no Inspector
    public Vector2 cursorHotspot = Vector2.zero; // Defina onde será o ponto de clique (hotspot)

    void Start()
    {
        // Definir o novo cursor
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }

    // Caso queira mudar o cursor em tempo de execução:
    public void SetCustomCursor()
    {
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }

    // Método para restaurar o cursor padrão do sistema
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
