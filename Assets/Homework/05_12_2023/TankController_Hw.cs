using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController_Hw : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody; // 물리적 연산이 적용가능한 유니티 data type 이자 GameObject 에 추가 가능한 Component
    private Vector3 moveDir;
    [Range(0, 50)]
    public int jumpForce;

    [Range(0, 50)]
    public int moveSpeed;

    [Header("회전각(Degrees)")]
    public float rotatingAngle;

    [Range(0, 20)]
    public float rotateSpeed;

    [SerializeField]
    private Camera camera;

    void Start()
    {
        // Rigidbody 가 생성되었으며, 해당 components 이 gameobj의 rigidbody 컴포넌트를 이미 콜링하고 있다고 가정한다 
        gameObject.name = "Player";
        
    }
    // Update is called once per frame
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
        //cameraComponent = gameObject.GetComponentInChildren<CameraController>();
    }

    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }

    private void Rotate()
    {
        //회전을 transform 으로 진행할 경우 
        transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime);
        // Where Rotate( Axis, float Angle, Space.Self by default) 
        camera.transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime);
        rotatingAngle = moveDir.x * rotateSpeed * Time.deltaTime;
    }

    private void OnJump(InputValue value)
    {
        Jump();
    }

    private void Jump()
    {
        rigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
    }

    private void Move()
    {
        //rb.AddForce(moveDir*movePower);
        //transform.Translate(moveDir * movePower * Time.deltaTime, Space.World);
        //transform.Translate(moveDir * movePower * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.forward * moveSpeed * moveDir.z * Time.deltaTime, Space.Self);
        // Where in Space.Self, front&back is determined by z axis,
    }

    private void OnDisable()
    {
        Debug.Log("탱크컨트롤러 잠시 종료"); 
    }

    private void OnEnable()
    {
        Debug.Log("탱크컨트롤러 시작");
    }
}
