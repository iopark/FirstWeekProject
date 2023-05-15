using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController_Hw : MonoBehaviour
{
    // Start is called before the first frame update
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

    [SerializeField]
    private Camera camera;

    void Start()
    {
        // Rigidbody �� �����Ǿ�����, �ش� components �� gameobj�� rigidbody ������Ʈ�� �̹� �ݸ��ϰ� �ִٰ� �����Ѵ� 
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
        //ȸ���� transform ���� ������ ��� 
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
        Debug.Log("��ũ��Ʈ�ѷ� ��� ����"); 
    }

    private void OnEnable()
    {
        Debug.Log("��ũ��Ʈ�ѷ� ����");
    }
}
