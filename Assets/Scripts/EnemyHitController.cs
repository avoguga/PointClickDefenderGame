using UnityEngine;

public class EnemyHitController : MonoBehaviour
{
    public Animator enemy_animator;  // Referência ao Animator

    // Método para ativar a animação de "Hit"
    public void TriggerHitAnimation()
    {
        if (enemy_animator != null)
        {
            enemy_animator.SetTrigger("Hit");  // Ativa o Trigger "Hit" no Animator
        }
    }
}
