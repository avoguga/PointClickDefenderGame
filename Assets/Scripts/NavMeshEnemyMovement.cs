using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemyMovement : MonoBehaviour
{
    public int enemy_max_hp;
    public float enemy_curr_hp;
    public int enemy_gold;
    public float enemy_speed;
    
    private NavMeshAgent agent;
    private int next_point = 0;

    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    agent.speed = enemy_speed;
    
    // Ajuste para jogos 2D
    agent.updateRotation = false;
    agent.updateUpAxis = false;

    enemy_curr_hp = enemy_max_hp;
    MoveToNextPoint();
}


    void Update()
    {
        // Verifica se o inimigo chegou ao ponto atual
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        // Verifica se ainda existem pontos a seguir
        if (next_point < MapPointsManager.Instance.map_points.Count)
        {
            agent.SetDestination(MapPointsManager.Instance.map_points[next_point].position);
            next_point++;
        }
        else
        {
            // Se não houver mais pontos, o inimigo pode ser destruído ou atingir um objetivo
        }
    }

    public void TakeDamage(float dmg)
    {
        enemy_curr_hp -= dmg;
        if (enemy_curr_hp <= 0)
        {
            WaveManager.Instance.n_monsters_left--;
            WaveManager.Instance.player_money += enemy_gold;
            WaveManager.Instance.UpdateHUD();
            Destroy(this.gameObject);
        }
    }
}
