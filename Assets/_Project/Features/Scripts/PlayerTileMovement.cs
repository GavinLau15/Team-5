using System.Collections;
using UnityEngine;

public class PlayerTileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    private Rigidbody2D rb;
    private Coroutine moveCoroutine;
    public Animator animator;
    public LayerMask whatStopsMovement;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = GetSnappedPosition(rb.position);
    }

    private void Update()
    {
        Move(GetDirection());
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

    private void Move(Vector2 direction)
    {
        // if player is moving already
        if (IsMoving)
        {
            return;
        }

        // if direction is zero
        if (direction == Vector2.zero)
        {
            StopMovement();
            return;
        }

        Vector2Int position = GetSnappedPosition(rb.position + direction);

        // if the overlap circle detects tile in whatStopsMovement layer, it prevents the movement.
        if (Physics2D.OverlapPoint(position, whatStopsMovement))
        {
            return;
        }

        // Start movement.
        moveCoroutine = StartCoroutine(MoveRoutine(position));
        SetAnimation(direction);
    }

    // to acheive moving one tile at a time
    private IEnumerator MoveRoutine(Vector2Int position)
    {
        IsMoving = true;

        // while distance between new position and current position is not zero
        while ((position - rb.position).sqrMagnitude > Mathf.Epsilon)
        {
            // current position move toward new position 
            rb.position = Vector2.MoveTowards(rb.position, position, speed * Time.deltaTime);
            yield return null;
        }
        
        // Snap to position at the end, avoiding floating error.
        rb.position = position;

        IsMoving = false;
    }

    public void StopMovement()
    {
        IsMoving = false;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        SetAnimation(Vector2.zero);
    }

    public void Teleport(Vector2 position)
    {
        StopMovement();
        TransitionManager.Instance.StartTransition(() => rb.position = position);
    }

    private void SetAnimation(Vector2 direction)
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.magnitude);
    }

    private Vector2Int GetSnappedPosition(Vector2 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }
}
