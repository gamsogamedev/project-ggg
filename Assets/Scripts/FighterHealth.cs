using UnityEngine;
using UnityEngine.UI;

public class FighterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

	[Header("Interface")]
    public Image healthBarFill;
	
	[Header("Barra Fantasma")]
    public Image ghostBarFill;
    public float tempoEsperaRastro = 0.5f;
    public float velocidadeRastro = 1.0f;
    private float timerRastro;
	
	[Header("Alerta de Vida Baixa")]
    public float porcentagemPerigo = 0.25f;
    public Color corNormal = Color.yellow;
    public Color corPerigo = Color.red;
    public float velocidadePiscar = 6f;
	
    private Animator anim;
    private FighterMovement movementScript;
    private FighterCombat combatScript;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movementScript = GetComponent<FighterMovement>();
        combatScript = GetComponent<FighterCombat>();
    }

    void Start()
    {
        currentHealth = maxHealth;

        if (anim != null) 
        {
            anim.SetInteger("currentHealth", currentHealth);
        }
		
		if (healthBarFill != null) healthBarFill.color = corNormal;
		UpdateHealthBar();
		
		if (ghostBarFill != null) ghostBarFill.fillAmount = 1f;
    }

	
	void Update()
    {

        if (ghostBarFill != null && healthBarFill != null)
        {
            if (timerRastro > 0)
            {

                timerRastro -= Time.deltaTime; 
            }
            else
            {

                if (ghostBarFill.fillAmount > healthBarFill.fillAmount)
                {
                    ghostBarFill.fillAmount -= velocidadeRastro * Time.deltaTime;
                }
            }
        }

        if (healthBarFill != null && currentHealth > 0)
        {
            float porcentagemAtual = (float)currentHealth / maxHealth;
            
            if (porcentagemAtual <= porcentagemPerigo)
            {

                float t = Mathf.PingPong(Time.time * velocidadePiscar, 1f);
                healthBarFill.color = Color.Lerp(corNormal, corPerigo, t);
            }
        }
    }
	
    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        if (anim != null) 
        {
            anim.SetInteger("currentHealth", currentHealth);
        }
		
		UpdateHealthBar();
		timerRastro = tempoEsperaRastro;
		

        if (currentHealth <= 0)
        {
			if (healthBarFill != null) healthBarFill.color = corPerigo;
            Die();
        }
    }
	
	private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} foi nocauteado!");
        
        if (movementScript != null) movementScript.enabled = false;
        if (combatScript != null) combatScript.enabled = false;

        Invoke("DisableCharacter", 2.0f); 
    }

    private void DisableCharacter()
    {
        gameObject.SetActive(false); 
    }

}