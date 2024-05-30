using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    //��ȣ�ۿ��� �������� �ؽ�Ʈ�� �����´�.
    public string GetInteractPrompt();
    //��ȣ�ۿ��ϴ� �Լ��̴�.
    public void OnInteract();
}

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; //�� �ʸ��� üũ�� ������
    private float lastCheckTime;    //�������� üũ�� �ð�
    public float maxCheckDistance;  //üũ�� �Ÿ�
    public LayerMask layerMask;     //���̾�

    //ĳ���� �ڷ��
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera _camera;

    void Start()
    {
        //���� ī�޶� ������
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Update������ Raycast�ϴ� ���� ����, 0.05�� �̻��� �Ǿ��� �� Raycast ����
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            //ī�޶� ���߾�
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            //����ĳ��Ʈ�� ������ ��
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                //�̹� ������ �ִ� ������ �ƴ϶��
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    //������ �����´�
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            //����ĳ��Ʈ�� ������ ��
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    //EŰ�� ������ ��
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        //�������� ĳ���õ��� ��
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            //��ȣ�ۿ� �� �Ŀ� ��� ����ִ� �ڵ�
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}