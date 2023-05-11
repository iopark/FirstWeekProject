using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController_2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody; // ������ ������ ���밡���� ����Ƽ data type ���� GameObject �� �߰� ������ Component
    [Range(0, 50)]
    public int jumpForce; 

    void Start()
    {
        // Rigidbody �� �����Ǿ�����, �ش� components �� gameobj�� rigidbody ������Ʈ�� �̹� �ݸ��ϰ� �ִٰ� �����Ѵ� 
        rigidbody.AddForce(Vector3.up*(jumpForce*2), ForceMode.Impulse);
        gameObject.name = "Player";
        Debug.Log("�����Ǽ� �⻵ �����մϴ� ��ũ��"); 
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
}
