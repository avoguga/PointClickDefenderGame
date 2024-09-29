using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjective : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Enemy")
    {
        // Reduz o HP do jogador quando o inimigo atinge o objetivo
        WaveManager.Instance.RemoveHP();

        // Atualiza o contador de inimigos restantes
        WaveManager.Instance.n_monsters_left--;

        // Destruir o inimigo diretamente, sem dar dinheiro
        Destroy(collision.gameObject);

        // Verificar se não há mais inimigos no mapa
        WaveManager.Instance.CheckIfAllEnemiesAreDead();
    }
}



}
