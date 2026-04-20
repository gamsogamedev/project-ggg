using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FighterMovement), typeof(FighterCombat))]
public class FighterInput : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference attackAction;

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
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (attackAction != null) attackAction.action.Disable();
    }

    void Update()
    {
        if (moveAction != null)
        {
            float direction = moveAction.action.ReadValue<float>();
            movement.SetMoveDirection(direction);
        }

        if (attackAction != null && attackAction.action.WasPressedThisFrame())
        {
            combat.PerformAttack();
        }
    }
}