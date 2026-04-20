using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FighterMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private float moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetMoveDirection(float direction)
    {
        moveDirection = direction;
    }

    void FixedUpdate() 
    {
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
        bool isMoving = Mathf.Abs(moveDirection) > 0.1f;

        if (anim != null) anim.SetBool("isWalking", isMoving);

        if (moveDirection > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDirection < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}