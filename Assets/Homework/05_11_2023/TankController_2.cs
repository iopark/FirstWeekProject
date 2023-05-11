using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController_2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody; // 물리적 연산이 적용가능한 유니티 data type 이자 GameObject 에 추가 가능한 Component
    [Range(0, 50)]
    public int jumpForce; 

    void Start()
    {
        // Rigidbody 가 생성되었으며, 해당 components 이 gameobj의 rigidbody 컴포넌트를 이미 콜링하고 있다고 가정한다 
        rigidbody.AddForce(Vector3.up*(jumpForce*2), ForceMode.Impulse);
        gameObject.name = "Player";
        Debug.Log("생성되서 기뻐 점프합니다 탱크가"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 이전프레임에 입력받은 값을 전달하며, 만약 KeyCode.Space가 맞다면 true 반환 
        {
            rigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse); // Where Addforce() 실시간으로 independent of deltaTime set by game, 해당 대상의 velocity 값을 반환합니다
                                                                         // (by the fixedDeltaTime, default = 0.02s, default mass = 9.81 N or 1 kg)
            Debug.Log("스페이스키 입력");
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>(); // 이미 해당 gameObject 에 rigidbody를 생성함과 동시에 rigidbody 와 상호작용을 일으킬 함수를 만들기
                                               // 위한 필수조건인, 
        jumpForce = 10; 
    }
}
