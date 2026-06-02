using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    [Header("Configurações de Combate")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
    
    [Header("Projétil (Magia)")]
    public GameObject Projetil;
    public Vector3 offsetProjetil = new Vector3(0, 1f, 0);
    
    [Header("Posições do Attack Point (Local)")]
    public Vector3 posicaoAtaqueNormal = new Vector3(0.5f, 0f, 0f);
    public Vector3 posicaoAtaqueCima = new Vector3(0.5f, 0.5f, 0f);
    public Vector3 posicaoAtaqueBaixo = new Vector3(0.5f, -0.5f, 0f);
    
    private bool isAttacking = false;

    // Memória de qual ataque está acontecendo para tocar o som certo
    private enum TipoAtaque { Normal, Cima, Baixo }
    private TipoAtaque ataqueAtual;

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

            // Grava o tipo de ataque e roda a animação correspondente
            if (yInput > 0.4f)
            {
                ataqueAtual = TipoAtaque.Cima;
                attackPoint.localPosition = posicaoAtaqueCima;
                anim.SetTrigger("AttackUp");
            }
            else if (yInput < -0.4f)
            {
                ataqueAtual = TipoAtaque.Baixo;
                attackPoint.localPosition = posicaoAtaqueBaixo;
                anim.SetTrigger("AttackDown");
            }
            else
            {
                ataqueAtual = TipoAtaque.Normal;
                attackPoint.localPosition = posicaoAtaqueNormal;
                anim.SetTrigger("Attack");
            }
        }
    }

    public void SpawnProjectile(float yInput, float direction)
    {
        if (Projetil != null)
        {
            GameObject cloneProjetil = Instantiate(Projetil, transform.position + offsetProjetil, Quaternion.identity);
            
            ProjectileProperties propriedades = cloneProjetil.GetComponent<ProjectileProperties>();
            
            if (propriedades != null)
            {
                propriedades.origin = gameObject;
                propriedades.direction = direction;
            }
        }
    }
    
    // Executa o dano no momento exato em que a mão bate no inimigo (via Evento de Animação)
    public void ExecuteDamage() 
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Evita que o jogador bata em si mesmo
            if (enemy.gameObject != this.gameObject)
            {
                FighterHealth health = enemy.GetComponent<FighterHealth>();
                if (health != null) 
                {
                    health.TakeDamage(attackDamage);
                    
                    // Toca o som de acordo com a memória do golpe atual
                    if (fighterAudio != null)
                    {
                        switch (ataqueAtual)
                        {
                            case TipoAtaque.Cima:
                                fighterAudio.PlayHitUpSound();
                                break;
                            case TipoAtaque.Baixo:
                                fighterAudio.PlayHitDownSound();
                                break;
                            case TipoAtaque.Normal:
                                fighterAudio.PlayHitNormalSound();
                                break;
                        }
                    }
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