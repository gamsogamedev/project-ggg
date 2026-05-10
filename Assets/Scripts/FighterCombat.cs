using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
	
	[Header("Posições do Attack Point (Local)")]
    public Vector3 posicaoAtaqueNormal = new Vector3(0.5f, 0f, 0f);
    public Vector3 posicaoAtaqueCima = new Vector3(0.5f, 0.5f, 0f);
    public Vector3 posicaoAtaqueBaixo = new Vector3(0.5f, -0.5f, 0f);
	
    private bool isAttacking = false;

    private Animator anim;
	private FighterAudio fighterAudio;

    void Awake()
    {
        anim = GetComponent<Animator>();
		fighterAudio = GetComponent<FighterAudio>();
    }

	public void PerformAttack(float yInput)
    {
        if (isAttacking) return;

        if (anim != null) 
        {
            isAttacking = true;

            if (yInput > 0.4f)
            {
                attackPoint.localPosition = posicaoAtaqueCima;
                anim.SetTrigger("AttackUp");
            }
            else if (yInput < -0.4f)
            {
                attackPoint.localPosition = posicaoAtaqueBaixo;
                anim.SetTrigger("AttackDown");
            }
            else
            {
                attackPoint.localPosition = posicaoAtaqueNormal;
                anim.SetTrigger("Attack");
            }
        }
    }
    
    public void ExecuteDamage() 
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            
            if (enemy.gameObject != this.gameObject)
            {
                FighterHealth health = enemy.GetComponent<FighterHealth>();
                if (health != null) 
                {
                    health.TakeDamage(attackDamage);
					fighterAudio.PlayHitSound();
                }
            }
        }
    }

    public void FinishAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}