using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMove : MonoBehaviour
{
    [Header("Move Related")]
    private Rigidbody rigidbody; // ������ ������ ���밡���� ����Ƽ data type ���� GameObject �� �߰� ������ Component
    private Vector3 moveDir;
    [Range(0, 50)]
    public int jumpForce;
    [Range(0, 50)]
    public int moveSpeed;
    [Header("ȸ����(Degrees)")]
    public float rotatingAngle;
    [Range(0, 20)]
    public float rotateSpeed;


    void Update()
    {
        Move();
        Rotate();
    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        moveSpeed = 1;
        jumpForce = 1;
        rotateSpeed = 15;
    }
    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }

    private void Rotate()
    {
        //ȸ���� transform ���� ������ ��� 
        transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime);
        // Where Rotate( Axis, float Angle, Space.Self by default) 
        rotatingAngle = moveDir.x * rotateSpeed * Time.deltaTime;
    }

    private void OnJump(InputValue value)
    {
        Debug.Log("JumpingFunction should work here");
        //Jump();
    }
    private void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * moveDir.z * Time.deltaTime, Space.Self);
    }
}
