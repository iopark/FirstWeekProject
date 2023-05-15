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
    private Transform bulletPoint; // �̷������� �����ϰ� ���ڷη� �����ϱ� ����� position �� ���ؼ��� UI �� ���ϰ� �������Ŀ� ������ unity������ �����ϴ�. 
    [SerializeField]
    private float repeatTime; // ���� interval �������ֱ� 
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private int bulletCount; //Initially fill up to 20 
    private int bulletLimit; // �ƸӸ� 20�߷� ���� ���� 
    [Header("Ammo Status")]
    public TextMeshProUGUI AmmoStatus;
    public int reloadTimeCounter;
    private bool reloading; 
    void Start()
    {
        // Rigidbody �� �����Ǿ�����, �ش� components �� gameobj�� rigidbody ������Ʈ�� �̹� �ݸ��ϰ� �ִٰ� �����Ѵ� 
        gameObject.name = "Player";
        bulletCount = bulletLimit;
        bulletLimit = 20;
        reloading = false; 

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
        if (reloading) // �������� �����϶��� �߻簡 �Ǹ� �ʹ� ���������
        {
            //���� ������� ����ó�� 
            //AmmoStatus.text = "Reloading";
            return; 
        }
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
        while (bulletCount > 0 && !reloading)
        {
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            //�ν��Ͻÿ���Ʈ: (������ �ν���Ʈ, #�����ε���׵�)�� ���ؼ� �ش���� ��� ������ �����ϰ� ���ִ� GameObject Function �̴�. 
            --bulletCount;
            SetText();
            yield return new WaitForSeconds(repeatTime);
        }
        if (reloading)
            AmmoStatus.text = "Reloading!"; 
        AmmoStatus.text = "Reload!"; 
        Debug.Log("Continuous Fire stopped: Ran out of Ammo"); 
    }

    /// <summary>
    /// ������ ���� �ڷ�ƾ. �����ӽð��� ������ 3�ʵ��� �����Ѵ�. 
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletReload()
    {
        //Reload takes 3 seconds. 
        int count = 1; 
        while (count < 4)
        {
            reloading = true; 
            AmmoStatus.text = $"Reloading Rounds : {count} /3"; 
            count++; 
            yield return new WaitForSeconds(1); //1�ʸ� ������ �ش� Ienumerator/ for loop MoveNext(), Current()�� �ݺ�ȣ���Ѵ�. Frame ���̿��� �����ϰ�. 
        }
        // ���� While(true) {} �̿��ٸ� frame�� ����� /�ٰ��ϴ� Update()���� ȣ���������ä�� ������ �����ѻ��°� �ɰ��̰ڴ�. 
        reloading = false; //������ �� �Ǿ��ٸ� �ٽ� false, ����� ��ӵǾ���ϴ�. 
        bulletCount = bulletLimit;
        SetText();
        Debug.Log("Reload End"); 
    }
    private void OnRepeatFire(InputValue value)
    {
        if (value.isPressed && !reloading) // ������� �������̶�� ����Ǹ� �ʹ� ��������� 
        {
            Debug.Log("button Pressed"); // Here, implement premade coroutine for the continuous fire 
            bulletRoutine = StartCoroutine(BulletMakeRoutine());
            // instance is saved, so it could be called back for stopping 
        }
        else
        {
            StopCoroutine(bulletRoutine); // Stop the saved instance of the coroutine 
            Debug.Log("Button letgo"); // �۾����� �׽�Ʈ/���� �׽�Ʈ�ϱ����� �ּ����� �� �ʿ��� �۾� 
        }
    }

    private void SetText()
    {
        //TMPro GUI �� .text �� �ʿ��� ������ �����ų�� �ִ�. 
        AmmoStatus.text = $"{bulletCount.ToString()}/{bulletLimit.ToString()}\t "; 

    }

    private void OnReload(InputValue value)
    {
        //Reload Ű�� �Էµɶ����� �����Ѵ� -> �ڷ�ƾ�� 
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
