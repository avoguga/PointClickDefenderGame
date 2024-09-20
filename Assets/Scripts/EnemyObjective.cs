using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjective : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            WaveManager.Instance.RemoveHP();
            collision.gameObject.GetComponent<EnemyMovement>().TakeDamage(collision.gameObject.GetComponent<EnemyMovement>().enemy_max_hp + 1);
        }
    }
}
