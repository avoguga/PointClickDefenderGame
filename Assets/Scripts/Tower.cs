using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int tower_price;
    public float attack_damage;
    public float attack_speed;
    public float attack_range;
    float attack_cooldown = 0;
    public GameObject projectile_;
    public GameObject target_enemy;

    // Building Mode
    bool is_building = true;
    int blocked_count;

    // Referência ao AudioSource para tocar som de construção
    private AudioSource buildSound;

    // Start is called before the first frame update
    void Start()
    {
        // Obter o AudioSource da torre
        buildSound = GetComponent<AudioSource>();

        // Inicializar a cor da torre em verde
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    // Update is called uma vez por frame
    void FixedUpdate()
    {
        if (!is_building)
        {
            Shoot();
        }
    }

    private void Update()
    {
        if (is_building)
        {
            BuidilngMode();
        }
    }

    void Shoot()
    {
        if (attack_cooldown > attack_speed)
        {
            if (target_enemy == null || Vector2.Distance(transform.position, target_enemy.transform.position) > attack_range)
            {
                target_enemy = FindTarget();
            }
            else
            {
                GameObject projectile_instance = Instantiate(projectile_, transform.position, Quaternion.identity);
                projectile_instance.GetComponent<Projectile>().projectile_damage = attack_damage;
                projectile_instance.GetComponent<Projectile>().target_ = target_enemy.transform;
                attack_cooldown = 0;
            }
        }
        else
        {
            attack_cooldown += Time.deltaTime;
        }
    }

    GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e_ in enemies)
        {
            if (Vector2.Distance(transform.position, e_.transform.position) < attack_range)
            {
                return e_;
            }
        }

        return null;
    }

    void BuidilngMode()
    {
        Vector3 mouse_position = Input.mousePosition;
        mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);
        mouse_position.z = transform.position.z;

        transform.position = mouse_position;

        if (blocked_count == 0 && WaveManager.Instance.player_money >= tower_price)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

            // Can Build
            if (Input.GetMouseButtonUp(0))
            {
                is_building = false;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                WaveManager.Instance.player_money -= tower_price;
                Debug.Log("Player Money after tower purchase: " + WaveManager.Instance.player_money); // Log do valor
                WaveManager.Instance.UpdateHUD();
                BuildingManager.Instance.building_ui.SetActive(true);

                // Tocar o som da construção
                if (buildSound != null)
                {
                    buildSound.Play();
                }
            }
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        
         // Cancelar o modo de construção ao clicar com o botão direito do mouse
    if (Input.GetMouseButtonDown(1)) // Botão direito do mouse
    {
        Debug.Log("Building mode cancelled");
        BuildingManager.Instance.building_ui.SetActive(true); // Mostrar a UI de construção novamente
        Destroy(this.gameObject); // Destruir a torre em construção
    }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BuildingManager.Instance.building_ui.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blocked")
        {
            blocked_count++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blocked")
        {
            blocked_count--;
        }
    }
}
