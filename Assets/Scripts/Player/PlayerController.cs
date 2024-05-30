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

        //�� �ڵ��� �ۼ������� y���� �̵��� ������ �� ������ ����ϱ� ������, 0������ ���� ������Ű�� ���ؼ� 
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    private void CameraLook()
    {
        //��, �Ʒ� �ü�
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        //camCurXRot�� -�� ���� ������ ���콺�� �����Ӱ� ���� �����̼� ������ �ݴ��̱� �����̴�.
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0.0f, 0.0f);

        //ĳ������ ���� �� ������ �����̰�
        transform.eulerAngles += new Vector3(0.0f, mouseDelta.x * lookSensitivity, 0.0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //CallbackContext�� ������¸� �޾ƿ� �� �ִ�.

        //-InputActionPhase-
        //Started = Ű�� ��������
        //Performed = Ű�� ������ ���� ���� ������ ������ �Ϸ���� ��
        //Canceled = ��ҵ��� ��
        //Waiting = ��ٸ��� �ִ� ���� ��
        //Disable = ������� ���� ��
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
        //å�� �ٸ�ó�� ĳ������ ��, ��, ��, �쿡 ��ġ�� ���̾ �ؿ��� ���
        Ray[] rays = new Ray[4]
        {
            //transform.up * 0.01f�� �� ������ �÷��̾ �׶��庸�� �׿� ���� ��츦 ����� ��¦ �÷��״�
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            //Ray, ����, ���̸���ũ
            if (Physics.Raycast(rays[i], 0.01f, groundLayerMask)) return true;
        }

        return false;
    }
}
