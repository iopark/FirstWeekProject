using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody; // 물리적 연산이 적용가능한 유니티 data type 이자 GameObject 에 추가 가능한 Component
    private Vector3 moveDir;
    private ControllerQueue controllerQueue;
    [Range(0, 50)]
    public int jumpForce;

    [Range(0, 50)]
    public int moveSpeed;


    void Start()
    {
        // Rigidbody 가 생성되었으며, 해당 components 이 gameobj의 rigidbody 컴포넌트를 이미 콜링하고 있다고 가정한다 
        gameObject.name = "Player";
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 이전프레임에 입력받은 값을 전달하며, 만약 KeyCode.Space가 맞다면 true 반환 
        {
            rigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            Debug.Log("스페이스키 입력");
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>(); // 이미 해당 gameObject 에 rigidbody를 생성함과 동시에 rigidbody 와 상호작용을 일으킬 함수를 만들기
                                               // 위한 필수조건인, 
        jumpForce = 10; 
    }

    private void OnMove(InputValue value)
    {

    }
    private void OnSwitchUnit(InputValue value)
    {
        controllerQueue = gameObject.GetComponent<ControllerQueue>();
        this.enabled = false;
        GameObject nextPlayable = controllerQueue.Switch(gameObject); 
        nextPlayable.GetComponent<PlayerInput>().enabled = true; 
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
