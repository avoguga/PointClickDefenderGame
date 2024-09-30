using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WaveManager : MonoBehaviour
{
    // Player Info
    public int player_hp;
    public int player_money;
    
    //Wave Info
    public List<WaveScriptable> waves_list;
    public int wave_;
    int n_monsters_spawned;
    public int n_monsters_left;
    public Transform spawn_location;
    public float spawn_cooldown;
    float spawn_cooldown_count;
    bool can_spawn_enemies = false;
    public GameObject next_wave_button;
    
    public int starting_wave = 0;


    public TMP_Text player_hp_text;
    public TMP_Text player_money_text;
    public TMP_Text wave_text;

    public GameObject game_over_screen;
    public GameObject victory_screen;

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        wave_ = starting_wave;
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        Debug.Log("Updating HUD - Player Money: " + player_money);
        player_hp_text.text = "HP: " + player_hp.ToString();
        player_money_text.text = "$: " + player_money.ToString();
        wave_text.text = "Wave: " + (wave_ + 1).ToString();
    }

    void FixedUpdate()
{
        Debug.Log("FixedUpdate is running");

    if (can_spawn_enemies)
    {
        SpawnEnemies(waves_list[wave_].monster);
    }
    
    CheckIfAllEnemiesAreDead(); // Verificação contínua
}

    public void StartWave()
    {

        wave_++;
        if (wave_ >= waves_list.Count)
        {
            Debug.Log("No more waves. Victory!");
            SceneManager.LoadScene("Victory"); 
            return;
        }

        Debug.Log("Starting Wave " + (wave_ + 1));

        // Esconder o botão ao iniciar a wave
        next_wave_button.SetActive(false);  
        n_monsters_left = waves_list[wave_].n_monsters;
        n_monsters_spawned = 0;
        can_spawn_enemies = true;

        // Atualizar o HUD com a nova wave
        UpdateHUD();
    }

    void SpawnEnemies(GameObject enemy)
    {
        if (n_monsters_left <= 0)
        {
            can_spawn_enemies = false;
            next_wave_button.SetActive(true); // Mostrar botão para a próxima wave
            return;
        }

        if (n_monsters_spawned < waves_list[wave_].n_monsters && spawn_cooldown_count < 0)
        {
            Instantiate(enemy, spawn_location.position, Quaternion.identity);
            n_monsters_spawned++;
            spawn_cooldown_count = spawn_cooldown;

            Debug.Log("Spawned enemy " + n_monsters_spawned + "/" + waves_list[wave_].n_monsters);
        }
        else
        {
            spawn_cooldown_count -= Time.deltaTime;
        }
    }

    public void RemoveHP()
    {
        player_hp--; // Reduz o HP do jogador

        if (player_hp <= 0)
        {
            GameOver(); // Se o HP for zero, chama o Game Over
        }

        UpdateHUD(); // Atualiza a interface com o novo valor de HP
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver"); // Carrega a cena de Game Over
    }

    // Verificação se todos os inimigos foram destruídos
  public void CheckIfAllEnemiesAreDead()
{
    // Verifica quantos inimigos ainda estão no mapa
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    
      // Log para saber quantos inimigos ainda estão vivos
    Debug.Log("Number of enemies on the map: " + enemies.Length);
    Debug.Log("Number of monsters left to spawn: " + n_monsters_left);

    // Se não houver mais inimigos, o botão de próxima wave deve aparecer
    if (enemies.Length == 0 && n_monsters_left <= 0)
    {
        Debug.Log("No more enemies. Showing Start Wave button.");
        next_wave_button.SetActive(true);
        can_spawn_enemies = false; // Pausar o spawn de inimigos
        Debug.Log("All enemies are dead. Showing Start Wave button.");
    }
}
}
