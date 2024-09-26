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
        UpdateHUD();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (can_spawn_enemies == true)
        {
            SpawnEnemies(waves_list[wave_].monster);
        }
    }

    public void UpdateHUD()
    {
        Debug.Log("Updating HUD - Player Money: " + player_money);
        player_hp_text.text = "HP: " + player_hp.ToString();
        player_money_text.text = "$: " + player_money.ToString();
        wave_text.text = "Wave: " + wave_.ToString();
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


    public void StarWave()
    {
        next_wave_button.SetActive(false);
        n_monsters_left = waves_list[wave_].n_monsters;
        n_monsters_spawned = 0;
        can_spawn_enemies = true;
    }

    void SpawnEnemies(GameObject enemy)
    {
        if(n_monsters_left <= 0)
        {
            if (wave_ >= waves_list.Count)
            {
                SceneManager.LoadScene("Victory"); // Carrega a cena Victory
            }
            can_spawn_enemies = false;
            wave_++;
            UpdateHUD();
            next_wave_button.SetActive(true);
            return;
        }
        
        if (n_monsters_spawned < waves_list[wave_].n_monsters && spawn_cooldown_count < 0)
        {
            Instantiate(enemy, spawn_location.position, Quaternion.identity);
            n_monsters_spawned++;
            spawn_cooldown_count = spawn_cooldown;
        }
        else
        {
            spawn_cooldown_count -= Time.deltaTime;
        }
    }
}
