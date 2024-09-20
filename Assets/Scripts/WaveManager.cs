using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    
    public int player_hp;
    public int player_money;
    public int wave_;
    
    public TMP_Text player_hp_text;
    public TMP_Text player_money_text;
    public TMP_Text wave_text;
    
    public GameObject game_over_screen;
    
      public static WaveManager Instance { get; private set; }
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    
    void Start()
    {
        UpdateHUD();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void UpdateHUD() {
        player_hp_text.text = "HP: " + player_hp.ToString();
        player_money_text.text = "$: " + player_money.ToString();
        wave_text.text = "Wave: " + wave_.ToString();
    }
    
    public void RemoveHP(){
        player_hp--;
        UpdateHUD();
        
        if (player_hp <= 0) {
            game_over_screen.SetActive(true);
        }
    }
}
