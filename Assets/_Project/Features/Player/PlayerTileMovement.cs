using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;

public class PlayerTileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    private Rigidbody2D rb;
    private Coroutine moveCoroutine;
    private bool isMoving = false;
    Vector2 movement;
    public Animator animator;

    public LayerMask whatStopsMovement;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);
        }

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
        // if player is moving already
        if (isMoving == true)
        {
            return;
        }

        // if direction is zero
        if (GetDirection() == Vector2.zero)
        {
            return;
        }

        Vector2 moveToPosition = rb.position + GetDirection();

        if (!Physics2D.OverlapPoint(moveToPosition, whatStopsMovement))
        {

            StopMovement();

            //if the overlap circle detects tile in whatStopsMovement layer, it prevents the movement.
            moveCoroutine = StartCoroutine(MoveHelper(moveToPosition));

        }

    }

    public void StopMovement()
    {

        if (moveCoroutine != null) 
        {
            StopCoroutine(moveCoroutine);
            
        }

        isMoving = false;
    }
    

    // to acheive moving one tile at a time
    IEnumerator MoveHelper(Vector2 newPos)
    {
        isMoving = true;

        // while distance between new position and current position is not zero
        while((newPos - rb.position).sqrMagnitude > Mathf.Epsilon)
        {

            
            //current position move toward new position 
            rb.position = Vector2.MoveTowards(rb.position, newPos, speed * Time.deltaTime);
            yield return null;
        }

        //rb.position = newPos;

        // add here

        isMoving = false;

    }

    public void Teleport(Vector2 position)
    {
        StopMovement();
        TransitionManager.Instance.StartTransition(() => rb.position = position);
    }

}
