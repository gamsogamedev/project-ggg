using UnityEngine;

public class BlobShadow : MonoBehaviour
{
    [Header("Alvo")]
    public Transform personagem;
    public LayerMask layerDoChao;

    [Header("Configurações")]
    public float distanciaMaxima = 5f;
    [Range(0f, 1f)] public float opacidadeMaxima = 1f;
    
    [Header("Escala da Sombra")]
    public Vector3 escalaNoChao = new Vector3(1.5f, 0.4f, 1f); 
    public float multiplicadorEscalaMinima = 0.4f;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (personagem == null) return;

        RaycastHit2D hit = Physics2D.Raycast(personagem.position, Vector2.down, distanciaMaxima, layerDoChao);

        if (hit.collider != null)
        {
            spriteRenderer.enabled = true;

            transform.position = new Vector3(personagem.position.x, hit.point.y, transform.position.z);

            float distanciaAtual = Vector2.Distance(personagem.position, hit.point);
            float ratio = Mathf.Clamp01(distanciaAtual / distanciaMaxima);

            transform.localScale = Vector3.Lerp(escalaNoChao, escalaNoChao * multiplicadorEscalaMinima, ratio);

            Color corAtual = spriteRenderer.color;
            corAtual.a = Mathf.Lerp(opacidadeMaxima, 0f, ratio);
            spriteRenderer.color = corAtual;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}