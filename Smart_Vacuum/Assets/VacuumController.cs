using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VacuumController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float checkDelay = 1f;
    private float lastMoveCheckTime;
    private Vector2 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseRandomDirection();
        lastMoveCheckTime = Time.time;
        lastPosition = rb.position;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;

        // Kiểm tra nếu robot bị kẹt
        if (Time.time - lastMoveCheckTime > checkDelay)
        {
            if (Vector2.Distance(rb.position, lastPosition) < 0.05f)
            {
                ChooseRandomDirection();
            }
            lastPosition = rb.position;
            lastMoveCheckTime = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ChooseRandomDirection();
    }

    void ChooseRandomDirection()
    {
        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(1, 1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, -1).normalized
        };

        Vector2 newDirection;
        do
        {
            newDirection = directions[Random.Range(0, directions.Length)];
        }
        while (newDirection == moveDirection);

        moveDirection = newDirection;
    }
}
