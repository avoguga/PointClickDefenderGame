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
    private AudioSource audioSource;

    // Referência ao Animator
    private Animator enemyAnimator;

    // Novo campo para o áudio de hit
    public AudioClip hitSound;

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

        // Obter o componente Animator
        enemyAnimator = GetComponent<Animator>();

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
        
        Debug.Log("Enemy took damage: " + dmg);

        enemy_curr_hp -= dmg;

        // Tocar o som de hit
        PlayHitSound();

        // Ativar a animação de "Hit"
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("HitFinal");  // Dispara a animação de "Hit"
        }

        // Se o inimigo morrer com este hit, ainda toca o som e depois destrói
        if (enemy_curr_hp <= 0)
        {
            
            Debug.Log("Enemy died. Giving gold: " + enemy_gold);
            // Dar dinheiro ao jogador quando o inimigo é destruído
            WaveManager.Instance.player_money += enemy_gold;
            WaveManager.Instance.UpdateHUD();
            
            // Atualizar o contador de inimigos restantes
        WaveManager.Instance.n_monsters_left--;
        Debug.Log("Enemy killed. Enemies left: " + WaveManager.Instance.n_monsters_left);

            // Reproduzir o som em um GameObject temporário
            PlayHitSoundAtDeath();

            // Destruir o inimigo imediatamente
            Destroy(gameObject);

            // Verificar se não há mais inimigos no mapa
            WaveManager.Instance.CheckIfAllEnemiesAreDead();
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

    // Função para reproduzir o som no último hit, mesmo que o inimigo seja destruído
    void PlayHitSoundAtDeath()
    {
        if (hitSound != null)
        {
            // Criar um GameObject temporário para reproduzir o som
            GameObject tempAudioSource = new GameObject("TempAudio");
            AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();

            tempSource.clip = hitSound;
            tempSource.Play();

            // Destruir o GameObject temporário após o som terminar
            Destroy(tempAudioSource, hitSound.length);
        }
    }
}
