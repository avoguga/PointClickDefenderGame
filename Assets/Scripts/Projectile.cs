using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectile_damage;
    public Transform target_;

    // Canon
    public bool is_canon = false;
    public float project_radius = 0;

    // Slow
    public bool is_slow = false;
    public float slow_rate = 0;

    // Rotação
    public float rotation_speed = 360f; // Velocidade de rotação em graus por segundo

    // Update is called once per frame
    void FixedUpdate()
    {
        // Verifica se o alvo ainda existe
        if (target_ == null)
        {
            // O alvo foi destruído, então destruímos o projétil
            Destroy(this.gameObject);
            return;
        }

        // Girar o projétil
        transform.Rotate(0, 0, rotation_speed * Time.deltaTime); // Gira no eixo Z (2D)

        // Mover o projétil em direção ao alvo
        transform.position = Vector3.MoveTowards(transform.position, target_.position, 4 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().TakeDamage(projectile_damage);

            // Canon
            if (is_canon == true)
            {
                Collider2D[] explosion_objects = Physics2D.OverlapCircleAll(transform.position, project_radius);
                foreach (Collider2D exp_obj in explosion_objects)
                {
                    if (exp_obj.gameObject.tag == "Enemy")
                    {
                        exp_obj.gameObject.GetComponent<EnemyMovement>().TakeDamage(projectile_damage);
                    }
                }
            }

            // Slow
            if (is_slow == true)
            {
                collision.gameObject.GetComponent<EnemyMovement>().enemy_speed *= 0.8f;
            }

            // Destruir o projétil após o impacto
            Destroy(this.gameObject);
        }
    }
}
