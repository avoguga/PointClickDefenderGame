using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public float attack_damage;
    public float attack_speed;
    public float attack_range;
    float attack_cooldown = 0;
    public GameObject projectile_;
    GameObject target_enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Shoot();
    }
    
    void Shoot()
    {
        if(attack_cooldown > attack_speed) 
        {
            if(target_enemy == null)
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
            if (Vector2.Distance(transform.position, e_.transform.position) <attack_range)
            {
                return e_;
            }
        }
        
        return null;
    }
}
