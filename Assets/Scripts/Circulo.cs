using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circulo : MonoBehaviour
{
    // Start is called before the first frame update
    	void Start()	{
		var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
