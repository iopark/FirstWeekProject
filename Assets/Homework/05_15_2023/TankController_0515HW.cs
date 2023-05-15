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
    private Transform bulletPoint; // 이런식으로 세세하게 숫자로로 조정하기 어려운 position 에 대해서는 UI 로 편리하게 조정이후에 응용이 unity에서는 가능하다. 
    [SerializeField]
    private float repeatTime; // 연사 interval 설정해주기 
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private int bulletCount; //Initially fill up to 20 
    private int bulletLimit; // 아머리 20발로 제한 설정 
    [Header("Ammo Status")]
    public TextMeshProUGUI AmmoStatus;
    public int reloadTimeCounter;
    private bool reloading; 
    void Start()
    {
        // Rigidbody 가 생성되었으며, 해당 components 이 gameobj의 rigidbody 컴포넌트를 이미 콜링하고 있다고 가정한다 
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
        if (reloading) // 장전중인 상태일때는 발사가 되면 너무 사기임으로
        {
            //가드 방식으로 예외처리 
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
            //인스턴시에이트: (생성전 인스턴트, #오버로드사항들)을 통해서 해당씬에 즉시 생성이 가능하게 해주는 GameObject Function 이다. 
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
    /// 장전을 위한 코루틴. 프레임시간과 별개로 3초동안 시행한다. 
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
            yield return new WaitForSeconds(1); //1초를 텀으로 해당 Ienumerator/ for loop MoveNext(), Current()를 반복호출한다. Frame 사이에서 교묘하게. 
        }
        // 만약 While(true) {} 이였다면 frame을 비롯한 /근거하는 Update()들은 호출되지않은채로 게임은 중지한상태가 될것이겠다. 
        reloading = false; //장전이 다 되었다면 다시 false, 사격은 계속되어야하니. 
        bulletCount = bulletLimit;
        SetText();
        Debug.Log("Reload End"); 
    }
    private void OnRepeatFire(InputValue value)
    {
        if (value.isPressed && !reloading) // 연사또한 장전중이라면 진행되면 너무 사기임으로 
        {
            Debug.Log("button Pressed"); // Here, implement premade coroutine for the continuous fire 
            bulletRoutine = StartCoroutine(BulletMakeRoutine());
            // instance is saved, so it could be called back for stopping 
        }
        else
        {
            StopCoroutine(bulletRoutine); // Stop the saved instance of the coroutine 
            Debug.Log("Button letgo"); // 작업도중 테스트/반응 테스트하기위한 최소한의 꼭 필요한 작업 
        }
    }

    private void SetText()
    {
        //TMPro GUI 에 .text 로 필요한 정보를 노출시킬수 있다. 
        AmmoStatus.text = $"{bulletCount.ToString()}/{bulletLimit.ToString()}\t "; 

    }

    private void OnReload(InputValue value)
    {
        //Reload 키가 입력될때마다 실행한다 -> 코루틴을 
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
