using System.Numerics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D))]
public class FighterMovement : MonoBehaviour
{
    [Header("Hitbox (Defesa Em Pé / Diagonal Cima)")]
    public Vector2 tamanhoBlockUp = new Vector2(1.2f, 1.8f);
    public Vector2 offsetBlockUp = new Vector2(0f, 0f);

    [Header("Hitbox (Defesa Agachado / Diagonal Baixo)")]
    public Vector2 tamanhoBlockDown = new Vector2(1.2f, 1f);
    public Vector2 offsetBlockDown = new Vector2(0f, -0.5f);

    [HideInInspector] public bool isBlockingButton;
    [HideInInspector] public Vector2 inputAtual;

	public float speed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform opponent;
    public Vector2 tamanhoCrouch = new Vector2(1f, 1f);
    public Vector2 offsetCrouch = new Vector2(0f,-0.5f);
    
    private Rigidbody2D rb;
    private Animator anim;
    private float moveDirection;
    private bool isGrounded;
	private bool isCrouching;
    private BoxCollider2D col;
    private Vector2 tamanhoNormal;
    private Vector2 offsetNormal;
    
    private FighterAudio fighterAudio;
    private bool wasGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        fighterAudio = GetComponent<FighterAudio>();
        
        if(col != null)
        {
            tamanhoNormal = col.size;
            offsetNormal = col.offset;
        }
    }

 public void SetMoveInput(Vector2 input)
    {
        inputAtual = input;
        moveDirection = input.x;
        isCrouching = input.y < -0.4f;

        if (anim != null) anim.SetBool("isCrouching", isCrouching);
        
        AtualizarHitbox();
    }

    public void AtualizarHitbox()
    {
        if (col == null) return;

        // Verifica se ta pressionado
        bool blockAtivo = isBlockingButton;

        // Checar block baixo
        bool blockBaixo = blockAtivo && inputAtual.y < -0.4f;
        
        // Block cima
        bool blockCima = blockAtivo && !blockBaixo;

        if (anim != null)
        {
            // Envia para o Animator
            anim.SetBool("isBlockingDown", blockBaixo);
            anim.SetBool("isBlockingUp", blockCima);
        }

        // Ajusta a hitbox
        if (blockAtivo)
        {
            if (blockBaixo)
            {
                col.size = tamanhoBlockDown;
                col.offset = offsetBlockDown;
            }
            else
            {
                col.size = tamanhoBlockUp;
                col.offset = offsetBlockUp;
            }
        }
        else
        {
            col.size = isCrouching ? tamanhoCrouch : tamanhoNormal;
            col.offset = isCrouching ? offsetCrouch : offsetNormal;
        }
    }
	
    void FixedUpdate() 
    {
        float horizontalVelocity = transform.localScale.x > 0 ? rb.linearVelocity.x : rb.linearVelocity.x * -1;
	
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		if (isGrounded && !wasGrounded)
        {
            if (fighterAudio != null) fighterAudio.PlayLandSound();
        }
		wasGrounded = isGrounded;
		
		if (anim != null) 
		{
			anim.SetBool("isGrounded", isGrounded);
			anim.SetFloat("verticalVelocity", rb.linearVelocity.y);
            anim.SetFloat("horizontalVelocity", horizontalVelocity);
		}

		
		float finalSpeed = (isCrouching || isBlockingButton) ? 0 : moveDirection * speed;
		
		rb.linearVelocity = new Vector2(finalSpeed, rb.linearVelocity.y);

		bool isMoving = Mathf.Abs(finalSpeed) > 0.1f;

        if (anim != null) anim.SetBool("isWalking", isMoving);

		HandleAutoFacing();
    }
	
	public void Jump()
	{
		if (isGrounded && !isCrouching)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
			if (anim != null) anim.SetTrigger("Jump");

                if (fighterAudio != null) fighterAudio.PlayJumpSound();
		}
	}
	
	private void HandleAutoFacing()
    {
        if (opponent == null) return;

        float directionToOpponent = opponent.position.x - transform.position.x;

        if (Mathf.Abs(directionToOpponent) < 0.1f) return;

        if (directionToOpponent > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        AtualizarHitbox();
    }
	
}