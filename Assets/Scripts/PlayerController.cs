using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private Rigidbody2D rb;  // Referência ao Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Pega o Rigidbody2D do Player
        targetPosition = transform.position;  // Inicializa a posição alvo com a posição inicial do Player
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // Mantém o eixo Z como 0 (pois estamos em um plano 2D)
        }
    }

    void FixedUpdate()
    {
        // Move o Player utilizando o Rigidbody2D para respeitar as colisões
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));
    }
}
