using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyMovement : MonoBehaviour
{
    public int enemy_max_hp;
    public float enemy_curr_hp;
    public int enemy_gold;
    public float enemy_speed;
    public SpriteRenderer enemy_sprite_renderer;
    int next_point = 0;

    private NavMeshAgent agent;
    private Color original_color;

    // Novo campo para o áudio de hit
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        enemy_curr_hp = enemy_max_hp;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemy_speed;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.radius = 0.3f;
        agent.avoidancePriority = Random.Range(30, 60);
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        SetNextDestination();

        if (enemy_sprite_renderer != null)
        {
            original_color = enemy_sprite_renderer.color;
        }

        // Inicializa o AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            SetNextDestination();
        }
    }

    void SetNextDestination()
    {
        if (next_point < MapPointsManager.Instance.map_points.Count)
        {
            Vector3 targetPosition = MapPointsManager.Instance.map_points[next_point].position;
            agent.SetDestination(targetPosition);
            next_point++;
        }
    }

    public void TakeDamage(float dmg)
    {
        enemy_curr_hp -= dmg;

        // Tocar o som de hit
        PlayHitSound();

        StartCoroutine(BlinkRed());

        if (enemy_curr_hp <= 0)
        {
            WaveManager.Instance.n_monsters_left--;
            WaveManager.Instance.player_money += enemy_gold;
            WaveManager.Instance.UpdateHUD();
            Destroy(this.gameObject);
        }
    }

    IEnumerator BlinkRed()
    {
        if (enemy_sprite_renderer != null)
        {
            // Desativar temporariamente o controle do Animator sobre o SpriteRenderer
            enemy_sprite_renderer.material.SetOverrideTag("RenderType", "Transparent");

            // Alterar a cor para vermelho
            enemy_sprite_renderer.color = Color.red;

            // Aguardar o tempo do efeito de hit
            yield return new WaitForSeconds(0.1f);

            // Restaurar a cor original
            enemy_sprite_renderer.color = original_color;

            // Reativar o controle do Animator
            enemy_sprite_renderer.material.SetOverrideTag("RenderType", "");
        }
    }

    // Função para tocar o som de hit
    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
