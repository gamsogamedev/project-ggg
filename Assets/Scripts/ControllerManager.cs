using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GerenciadorControles : MonoBehaviour
{
    [Header("Arraste os personagens aqui")]
    public PlayerInput player1;
    public PlayerInput player2;

    [Header("Nome exato do Esquema (ex: Gamepad ou Controle)")]
    public string nomeDoEsquema = "Gamepad"; 

    IEnumerator Start()
    {
        // O SEGREDO: Espera o jogo carregar o primeiro frame. 
        // Isso dá tempo do Unity criar os "Users" e evita o erro de Invalid User!
        yield return new WaitForEndOfFrame();

        if (Gamepad.all.Count >= 2)
        {
            // Impede que eles tentem trocar de controle sozinhos no meio da luta
            player1.neverAutoSwitchControlSchemes = true;
            player2.neverAutoSwitchControlSchemes = true;

            // Entrega o Controle 1 (Índice 0) pro Player 1
            player1.SwitchCurrentControlScheme(nomeDoEsquema, Gamepad.all[0]);
            
            // Entrega o Controle 2 (Índice 1) pro Player 2
            player2.SwitchCurrentControlScheme(nomeDoEsquema, Gamepad.all[1]);

        }
        else
        {
            Debug.LogWarning("<color=orange>ATENÇÃO: Apenas " + Gamepad.all.Count + " controle(s) detectado(s).</color>");
        }
    }
}