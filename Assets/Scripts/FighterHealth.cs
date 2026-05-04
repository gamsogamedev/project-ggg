using UnityEngine;

public class FighterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

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

    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        if (anim != null) 
        {
            anim.SetInteger("currentHealth", currentHealth);
        }

        Debug.Log($"{gameObject.name} tomou {damage} de dano! Vida: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} foi nocauteado!");
            Die();
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