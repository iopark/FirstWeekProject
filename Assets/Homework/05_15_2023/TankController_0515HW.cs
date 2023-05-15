using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController_0515HW : MonoBehaviour
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

    [Header("Shooter")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform bulletPoint; // �̷������� ���� stat ���� �����ϱ� ����� position �� ���ؼ��� UI �� ������ ����������. 
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
        // Rigidbody �� �����Ǿ�����, �ش� components �� gameobj�� rigidbody ������Ʈ�� �̹� �ݸ��ϰ� �ִٰ� �����Ѵ� 
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
        Debug.Log("��ũ��Ʈ�ѷ� ��� ����"); 
    }

    private void OnEnable()
    {
        Debug.Log("��ũ��Ʈ�ѷ� ����");
    }
}
