using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController_Hw : MonoBehaviour
{
    // Message Event �Լ������ε� �ش� ��ũ��Ʈ�� ��������� �������ִ°��� ���� 
    private Vector3 moveDir;
    private Rigidbody rb;
    private float rotateVal;

    [Header("ȸ����(Degrees)")]
    public float rotatingAngle; 

    [SerializeField]
    private Camera camera; 

    //private Component cameraComponent; 
    [Range(0, 20)]
    public float movePower;

    [Range(0, 20)]
    public float jumpPower;

    [Range(0, 20)]
    public float rotateSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // �̹� �ش� gameObject �� rigidbody�� �����԰� ���ÿ� rigidbody �� ��ȣ�ۿ��� ����ų �Լ��� �����
                                        // ���� �ʼ�������, 
        jumpPower = 10;
        movePower = 10;
    }

    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y; 
    }

    private void Update()
    {
        Move();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }


    private void Move()
    {
        rb.AddForce(moveDir * movePower);
    }

    private void Rotate()
    {
        //ȸ���� transform ���� ������ ��� 
        transform.Rotate(Vector3.up, moveDir.x *rotateSpeed * Time.deltaTime);
        // Where Rotate( Axis, float Angle, Space.Self by default) 
        //camera.transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime);
        //rotatingAngle = moveDir.x * rotateSpeed * Time.deltaTime; 
    }

    private void Rotation()
    {
        transform.rotation = Quaternion.identity; // assumes value as (0,0,0)  
        // transform.position = new Vector3(0,0,0); 
        transform.rotation = Quaternion.Euler(0, 90, 0);
        //Euler ������ Quaternion ���� ��ȯ 
        //transform.rotation.ToEulerAngles(); 
        //Quaternion angle => Euler's Angle 
    }
    /// <summary>
    /// Will only respond if Unity engine sends message corresponding to the OnJump() Function material, decided by the INput System 
    /// </summary>
    /// <param name="value"></param>
    private void OnJump(InputValue value)
    {
        // ��ü������ OnJump �� OnJump �� �ش��ϴ� �Է°��� ���� �޼����� �߻��������� ���� ���� 
        Jump(); 
        //Jump(); 
        //rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        Debug.Log("��Ʈ�ѷ� ��� ����");
    }

    private void OnEnable()
    {
        Debug.Log("��Ʈ�ѷ� ����");
    }
}
