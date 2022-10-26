using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            direction = direction + Vector2.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = direction + Vector2.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = direction + Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = direction + Vector2.left;
        }

        rb.velocity = speed * direction;
    }
}
