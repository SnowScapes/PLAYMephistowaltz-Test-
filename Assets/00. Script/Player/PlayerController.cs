using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private Rigidbody rigidBody;

    private PlayerInput input => InputManager.Instance.Input;
    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraPos = new Vector3(0, 20, -2);
    private Vector3 aimPos;

    // InputAction의 각 Action에 실행될 메서드 구독 및 Input 활성화
    private void OnEnable()
    {
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        input.Player.Look.performed += OnLook;
        input.Player.Shoot.performed += OnShoot;
        input.Player.Enable();
    }

    // InputAction의 각 Action에 실행될 메서드 구독해제 및 Input 비활성화
    private void OnDisable()
    {
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Player.Look.performed -= OnLook;
        input.Player.Shoot.performed -= OnShoot;
        input.Player.Disable();
    }

    private void FixedUpdate()
    {
        // rigidBody의 Position과 Rotation을 관리하는 것이기에
        // FixedUpdate를 활용
        Move();
        Look();
    }

    private void Move()
    {
        if (velocity != Vector3.zero)
        {
            rigidBody.MovePosition(rigidBody.position + velocity);
            Camera.main.transform.position = rigidBody.position + cameraPos;
        }
    }

    private void Look()
    {
        rigidBody.rotation = Quaternion.LookRotation(aimPos);
    }

    // rigidBody를 가진 오브젝트이기에 Rigidbody.MovePosition을 활용
    private void OnMove(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector3>().normalized * playerStat.MoveSpeed * Time.fixedDeltaTime;
    }

    private void OnLook(InputAction.CallbackContext context) 
    {
        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            aimPos = hit.point;
            //조준 위치 y값 보정 
            aimPos.y = rigidBody.transform.position.y;
            aimPos -= rigidBody.transform.position;
            aimPos = aimPos.normalized;
        }
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        
    }
}
