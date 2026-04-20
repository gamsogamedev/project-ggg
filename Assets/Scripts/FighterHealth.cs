using UnityEngine;

public class FighterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} tomou {damage} de dano! Vida: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} foi nocauteado!");
            gameObject.SetActive(false); 
        }
    }
}