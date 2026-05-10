using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FighterMovement), typeof(FighterCombat))]
public class FighterInput : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference attackAction;
	public InputActionReference jumpAction;
	
    private FighterMovement movement;
    private FighterCombat combat;

    void Awake()
    {
        movement = GetComponent<FighterMovement>();
        combat = GetComponent<FighterCombat>();
    }

    void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (attackAction != null) attackAction.action.Enable();
		if (jumpAction != null) jumpAction.action.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (attackAction != null) attackAction.action.Disable();
		if (jumpAction != null) jumpAction.action.Disable();
    }

    void Update()
    {
		Vector2 moveInput = Vector2.zero;
		
        if (moveAction != null)
        {
			moveInput = moveAction.action.ReadValue<Vector2>();
            
            movement.SetMoveDirection(moveInput.x);
			
			bool isCrouching = moveInput.y < -0.5f && Mathf.Abs(moveInput.x) < 0.4f;
            movement.SetCrouch(isCrouching);
			
        }
		
		if (jumpAction != null && jumpAction.action.WasPressedThisFrame())
        {
            movement.Jump();
        }
		
        if (attackAction != null && attackAction.action.WasPressedThisFrame())
        {
            combat.PerformAttack(moveInput.y);
        }
    }
}