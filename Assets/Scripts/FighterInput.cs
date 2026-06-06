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
        
        movement.SetMoveDirection(moveInput.x);
        
        bool isCrouching = moveInput.y < -0.5f && Mathf.Abs(moveInput.x) < 0.4f;
        movement.SetCrouch(isCrouching);
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