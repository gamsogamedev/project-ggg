using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FighterMovement : MonoBehaviour
{
	public float speed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform opponent;
    
    private Rigidbody2D rb;
    private Animator anim;
    private float moveDirection;
    private bool isGrounded;

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
	
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		
		if (anim != null) anim.SetBool("isGrounded", isGrounded);
		
		
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
		
        bool isMoving = Mathf.Abs(moveDirection) > 0.1f;

        if (anim != null) anim.SetBool("isWalking", isMoving);

		HandleAutoFacing();
    }
	
	public void Jump()
	{
		if (isGrounded)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
			if (anim != null) anim.SetTrigger("Jump");
		}
	}
	
	private void HandleAutoFacing()
    {
        if (opponent == null) return;

        float directionToOpponent = opponent.position.x - transform.position.x;

        if (Mathf.Abs(directionToOpponent) < 0.1f) return;

        if (directionToOpponent > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
	
}