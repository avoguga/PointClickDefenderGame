using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointsManager : MonoBehaviour
{
    
    public List<Transform> map_points;
    
    public static MapPointsManager Instance { get; private set; }
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
