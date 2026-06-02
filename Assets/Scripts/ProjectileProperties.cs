using System.Collections;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using Vector2 = UnityEngine.Vector2;


public class ProjectileProperties : MonoBehaviour
{

    public GameObject origin;

    public float speed = 7f;
    public float direction;
    
    // direção dependente da posição do oponente; Aqui pelo HandleAutoFacing estar private
    public void Update()
    {
        Vector2 vector = transform.right;
        if(direction > 0)
        {
            GetComponent<Rigidbody2D>().position += 5 * Time.deltaTime * vector;
        }
        else
        {
            GetComponent<Rigidbody2D>().position += 5 * Time.deltaTime * -vector;
        }
    }

    // checagem de colisão e dano se colidir com oponente
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != origin && collision.gameObject.TryGetComponent<FighterHealth>(out FighterHealth health))
        {
            health.TakeDamage(6);

            if (origin != null && origin.TryGetComponent<FighterAudio>(out FighterAudio audioAtacante))
            {
                audioAtacante.PlayHitNormalSound();
            }
            
            Destroy(gameObject);
        }
        StartCoroutine(RecycleProjectile(3));
    }

    // timer para destruir o projétil
    IEnumerator RecycleProjectile(float timer)
    {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    } 

}
