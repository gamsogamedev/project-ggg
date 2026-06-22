using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FighterMovement), typeof(FighterCombat))]
public class FighterInput : MonoBehaviour
{
    private FighterMovement movement;
    private FighterCombat combat;
    
    private Vector2 moveInput;

    void Awake()
    {
        movement = GetComponent<FighterMovement>();
        combat = GetComponent<FighterCombat>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
        movement.SetMoveInput(moveInput); 
    }

    public void OnBlock(InputValue value)
    {
        movement.isBlockingButton = value.isPressed;
        movement.AtualizarHitbox();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            movement.Jump();
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            combat.PerformAttack(moveInput.y);
        }
    }

    public void OnWpnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            combat.ThrowProjectile();
        }
    }
}