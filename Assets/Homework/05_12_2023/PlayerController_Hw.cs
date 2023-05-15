using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController_Hw : MonoBehaviour
{
    // Message Event 함수만으로도 해당 스크립트가 실행됨으로 삭제해주는것이 국룰 
    private Vector3 moveDir;
    private Rigidbody rb;
    private float rotateVal;

    [Header("회전각(Degrees)")]
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
        rb = GetComponent<Rigidbody>(); // 이미 해당 gameObject 에 rigidbody를 생성함과 동시에 rigidbody 와 상호작용을 일으킬 함수를 만들기
                                        // 위한 필수조건인, 
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
        //회전을 transform 으로 진행할 경우 
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
        //Euler 각도를 Quaternion 으로 변환 
        //transform.rotation.ToEulerAngles(); 
        //Quaternion angle => Euler's Angle 
    }
    /// <summary>
    /// Will only respond if Unity engine sends message corresponding to the OnJump() Function material, decided by the INput System 
    /// </summary>
    /// <param name="value"></param>
    private void OnJump(InputValue value)
    {
        // 객체적으로 OnJump 는 OnJump 에 해당하는 입력값에 대한 메세지가 발생했을때에 대해 구현 
        Jump(); 
        //Jump(); 
        //rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        Debug.Log("컨트롤러 잠시 종료");
    }

    private void OnEnable()
    {
        Debug.Log("컨트롤러 시작");
    }
}
