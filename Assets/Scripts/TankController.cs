using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody; // ������ ������ ���밡���� ����Ƽ data type ���� GameObject �� �߰� ������ Component
    private Vector3 moveDir;
    private ControllerQueue controllerQueue;
    [Range(0, 50)]
    public int jumpForce;

    [Range(0, 50)]
    public int moveSpeed;


    void Start()
    {
        // Rigidbody �� �����Ǿ�����, �ش� components �� gameobj�� rigidbody ������Ʈ�� �̹� �ݸ��ϰ� �ִٰ� �����Ѵ� 
        gameObject.name = "Player";
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ���������ӿ� �Է¹��� ���� �����ϸ�, ���� KeyCode.Space�� �´ٸ� true ��ȯ 
        {
            rigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            Debug.Log("�����̽�Ű �Է�");
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>(); // �̹� �ش� gameObject �� rigidbody�� �����԰� ���ÿ� rigidbody �� ��ȣ�ۿ��� ����ų �Լ��� �����
                                               // ���� �ʼ�������, 
        jumpForce = 10; 
    }

    private void OnMove(InputValue value)
    {

    }
    private void OnSwitchUnit(InputValue value)
    {
        controllerQueue = gameObject.GetComponent<ControllerQueue>();
        this.enabled = false;
        GameObject nextPlayable = controllerQueue.Switch(gameObject); 
        nextPlayable.GetComponent<PlayerInput>().enabled = true; 
    }

    private void OnDisable()
    {
        Debug.Log("��ũ��Ʈ�ѷ� ��� ����"); 
    }

    private void OnEnable()
    {
        Debug.Log("��ũ��Ʈ�ѷ� ����");
    }
}
