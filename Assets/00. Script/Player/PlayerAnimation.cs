using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerInput input => InputManager.Instance.Input;
    
    private void OnEnable()
    {
        input.Player.Move.performed += MoveAnimation;
        input.Player.Move.canceled += MoveAnimation;
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= MoveAnimation;
        input.Player.Move.canceled -= MoveAnimation;
    }

    private void MoveAnimation(InputAction.CallbackContext context)
    {
        Vector3 velocity = context.ReadValue<Vector3>();

        // input 값이 0이 아닐 경우 움직이는 애니메이션 재생
        animator.SetBool("Move", velocity.magnitude != 0);
    }
}
