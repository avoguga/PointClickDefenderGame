using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int enemy_max_hp;
    public float enemy_curr_hp;
    public float enemy_speed;
    int next_point = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemy_curr_hp = enemy_max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, MapPointsManager.Instance.map_points[next_point].position, enemy_speed * Time.deltaTime);
    
        if (transform.position == MapPointsManager.Instance.map_points[next_point].position) {
            next_point++;
        }
    }
    
    public void TakeDamage(float dmg) {
        enemy_curr_hp -= dmg;
        if (enemy_curr_hp <= 0) {
            Destroy(this.gameObject);
        }
    }
}
