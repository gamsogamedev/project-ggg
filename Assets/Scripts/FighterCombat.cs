using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
    private bool isAttacking = false;

    private Animator anim;
	private FighterAudio fighterAudio;

    void Awake()
    {
        anim = GetComponent<Animator>();
		fighterAudio = GetComponent<FighterAudio>();
    }

    public void PerformAttack()
    {
        if (isAttacking) return;

        if (anim != null) 
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
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