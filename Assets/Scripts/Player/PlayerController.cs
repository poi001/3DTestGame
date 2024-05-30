using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;

        //이 코드의 작성이유는 y축의 이동은 점프를 할 때에만 사용하기 때문에, 0정도의 값을 고정시키기 위해서 
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    private void CameraLook()
    {
        //위, 아래 시선
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        //camCurXRot에 -를 붙인 이유는 마우스의 움직임과 실제 로테이션 값과는 반대이기 떄문이다.
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0.0f, 0.0f);

        //캐릭터의 고개가 양 옆으로 움직이게
        transform.eulerAngles += new Vector3(0.0f, mouseDelta.x * lookSensitivity, 0.0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //CallbackContext는 현재상태를 받아올 수 있다.

        //-InputActionPhase-
        //Started = 키가 눌렸을때
        //Performed = 키가 눌리고 나서 내부 로직이 실행이 완료됐을 때
        //Canceled = 취소됐을 때
        //Waiting = 기다리고 있는 중일 때
        //Disable = 사용하지 못할 때
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        //책상 다리처럼 캐릭터의 앞, 뒤, 좌, 우에 위치한 레이어를 밑에다 쏜다
        Ray[] rays = new Ray[4]
        {
            //transform.up * 0.01f를 한 이유는 플레이어가 그라운드보다 및에 있을 경우를 대비해 살짝 올려뒀다
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            //Ray, 길이, 레이마스크
            if (Physics.Raycast(rays[i], 0.01f, groundLayerMask)) return true;
        }

        return false;
    }
}
