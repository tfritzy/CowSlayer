using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float MovementSpeed;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        this.rb.velocity = GetKeyboardInput() * MovementSpeed;
    }

    private Vector2 GetKeyboardInput()
    {
        Vector2 movementDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x += 1;
        }
        return movementDirection;
    }
}
