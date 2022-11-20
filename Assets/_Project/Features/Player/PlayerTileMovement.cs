using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    private Rigidbody2D rb;

    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private Vector2 GetDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }

        return direction.normalized;
    }

    private void Move()
    {
        if (isMoving == true)
        {
            return;
        }

        if (GetDirection() == Vector2.zero)
        {
            return;
        }

        Vector2 moveToPosition = rb.position + GetDirection();
        StartCoroutine(MoveHelper(moveToPosition));

    }


    IEnumerator MoveHelper(Vector2 newPos)
    {
        isMoving = true;

        while((newPos - rb.position).sqrMagnitude > Mathf.Epsilon)
        {
            rb.position = Vector2.MoveTowards(rb.position, newPos, speed * Time.fixedDeltaTime);
            yield return null;
        }

        rb.position = newPos;

        isMoving = false;


    }
}
