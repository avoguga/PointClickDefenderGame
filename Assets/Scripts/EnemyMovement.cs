using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    int next_point = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, MapPointsManager.Instance.map_points[next_point].position, 4 * Time.deltaTime);
    
        if (transform.position == MapPointsManager.Instance.map_points[next_point].position) {
            next_point++;
        }
    }
}
