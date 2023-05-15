using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController_0515HW : MonoBehaviour
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

    [Header("Shooter")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform bulletPoint; // 이런식으로 그저 stat 으로 조정하기 어려운 position 에 대해서는 UI 로 조정이 가능해진다. 
    [SerializeField]
    private float repeatTime; 
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private int bulletCount; //Initially fill up to 20 
    private int bulletLimit;
    [Header("Ammo Status")]
    public TextMeshProUGUI AmmoStatus;
    public int reloadTimeCounter; 
    void Start()
    {
        // Rigidbody 가 생성되었으며, 해당 components 이 gameobj의 rigidbody 컴포넌트를 이미 콜링하고 있다고 가정한다 
        gameObject.name = "Player";
        bulletCount = bulletLimit;
        bulletLimit = 20;

    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    private void Awake()
    {
        reloadTimeCounter = 0;
        repeatTime = 0.5f; 
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
    private void OnFire(InputValue value)
    {
        Debug.Log("Fire");
        //GameObject obj =  Instantiate(bulletPrefab); // GameObject.Function: Instantiate the targeting object 
        //obj.transform.position = transform.position;
        //obj.transform.rotation = transform.rotation;
        if (bulletCount <= 0)
        {
            Debug.Log("Ran out of Ammo");
            AmmoStatus.text = "Reload!";
            return; 
        }
        --bulletCount; 
        Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        SetText(); 
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

    private Coroutine bulletRoutine;
    private Coroutine bulletReload; 

    IEnumerator BulletMakeRoutine()
    {
        while (bulletCount > 0)
        {
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            --bulletCount;
            SetText();
            yield return new WaitForSeconds(repeatTime);
        }
        AmmoStatus.text = "Reload!"; 
        Debug.Log("Continuous Fire stopped: Ran out of Ammo"); 
    }

    IEnumerator BulletReload()
    {
        //Reload takes 3 seconds. 
        int count = 1; 
        while (count < 4)
        {
            AmmoStatus.text = $"Reloading Rounds : {count} /3"; 
            count++; 
            yield return new WaitForSeconds(1); 
        }
        bulletCount = bulletLimit;
        SetText();
        Debug.Log("Reload End"); 
    }
    private void OnRepeatFire(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("button Pressed"); // Here, implement premade coroutine for the continuous fire 
            bulletRoutine = StartCoroutine(BulletMakeRoutine());
            // instace is saved, so it could be called back for stopping 
        }
        else
        {
            StopCoroutine(bulletRoutine); // Stop the saved instance of the coroutine 
            Debug.Log("Button letgo");
        }
    }

    private void SetText()
    {
        AmmoStatus.text = $"{bulletCount.ToString()}/{bulletLimit.ToString()}\t "; 

    }

    private void OnReload(InputValue value)
    {
        Debug.Log("Reload Start"); 
        StartCoroutine(BulletReload());
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
