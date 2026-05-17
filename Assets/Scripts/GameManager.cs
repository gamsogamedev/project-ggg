using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI do Timer")]
    public TextMeshProUGUI timerText;
    public float tempoDeRound = 99f;

    [Header("UI de Game Over")]
    public GameObject painelGameOver;
    public TextMeshProUGUI textoVencedor;

    [Header("Referências dos Players")]
    public FighterHealth player1;
    public FighterHealth player2;

    private bool jogoAcabou = false;

    void Start()
    {
        Time.timeScale = 1f;
        if (painelGameOver != null) painelGameOver.SetActive(false);
    }

    void Update()
    {
        if (jogoAcabou) return;

        ControlarTimer();
        VerificarMorte();
    }

    void ControlarTimer()
    {
        if (tempoDeRound > 0)
        {
            tempoDeRound -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(tempoDeRound).ToString();
        }
        else
        {
            tempoDeRound = 0;
            timerText.text = "0";
            VerificarVencedorPorTempo();
        }
    }

    void VerificarMorte()
    {
        if (player1.currentHealth <= 0)
        {
            FinalizarPartida(2);
        }
        else if (player2.currentHealth <= 0)
        {
            FinalizarPartida(1);
        }
    }

    void VerificarVencedorPorTempo()
    {
        if (player1.currentHealth > player2.currentHealth)
        {
            FinalizarPartida(1);
        }
        else if (player2.currentHealth > player1.currentHealth)
        {
            FinalizarPartida(2);
        }
        else
        {
            FinalizarPartida(0);
        }
    }

	void FinalizarPartida(int vencedor)
    {
        jogoAcabou = true;
        
        StartCoroutine(RotinaFimDeJogo(vencedor));
    }

    IEnumerator RotinaFimDeJogo(int vencedor)
    {
        yield return new WaitForSeconds(2f); 

        painelGameOver.SetActive(true);

        if (vencedor == 1)
        {
            textoVencedor.text = "PLAYER 1 WINS!";
            textoVencedor.color = Color.red; 
        }
        else if (vencedor == 2)
        {
            textoVencedor.text = "PLAYER 2 WINS!";
            textoVencedor.color = Color.blue; 
        }
        else
        {
            textoVencedor.text = "DRAW!";
            textoVencedor.color = Color.white;
        }
		
        Time.timeScale = 0f; 
    }
}