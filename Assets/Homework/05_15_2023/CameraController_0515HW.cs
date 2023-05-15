using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CameraController_0515HW : MonoBehaviour
{
    public GameObject player;

    [Range(-20f, -5f)]
    public float offset;
    private float rotatingangle; 
    private Vector3 cameraPos; 
    private Component getPointer; 
    private TankController playerController;
    private float fixedY;
    [SerializeField]
    private TextMeshProUGUI countText; //탱크의 포탄 갯수를 세기 위한 ui 삽입 
    // Start is called before the first frame update
    void Start()
    {
        //Given 해당 카메라의 position 값과, player의 position값이 있다고 전제를 하는 기능이 있기에, Awake보다는 Start가 적절하다. 
        //cameraPos = transform.position;
        fixedY = transform.position.y;
        offset = -10f; 
        getPointer = player.GetComponentInChildren<SightEdge>(); 
        playerController = player.GetComponentInChildren<TankController>();
        TextMeshProUGUI test = Instantiate(countText, transform.position, transform.rotation); // 해당 TMPro 생성
        transform.parent = test.transform; // 부모 tranform 설정해주어서 
        test.transform.localScale = transform.localScale;  // text 파일 크기 설정 
    }

    // Update 는 비록 새로운 프레임 이전에 호출되지만, Delegate += function 처럼 순서를 보장하지는 못한다. 
    // 특히 Camera offset 값에 따라 카메라의 위치가 결정될때, 일반적인 물리연산함수이후에 Update()가 되어야 하는데, 이를 보장하지 못한다. 
    // 따라서 해당scene 의 모든 Game object 의 메세지함수중, Update() 가 다 끝난 이후에 진행되는 FixedUpdate()를 사용하는것이 일반적이겠다. 
    void Update()
    {
    }

    
    void LookAt()
    {
        //Vector3 sightLimit = new Vector3
        //transform.LookAt(new Vector3 (player.transform.po);
        transform.LookAt(getPointer.transform.position); 
    }

    private void LateUpdate()
    {
        Vector3 positionOA = getPointer.transform.position - player.transform.position;
        cameraPos = (positionOA.normalized * offset); // 1,2,3,4 적용 
        //cameraPos.y = fixedY;
        //최근들어 Unity는 Radian을 지닌 Quaternion.EulerAngle을 찬밥신세취급하며 Euler를 사용하라고 사실상 떠밀고 있다. 
        cameraPos = Quaternion.AngleAxis(playerController.rotatingAngle, player.transform.up) * cameraPos; // 카메라-탱크-시야 대칭값을 추적 = 탱크-시야 회전값에 반대되는 좌표 * 카메라-탱크 고정벡터값
        transform.position = cameraPos + player.transform.position + new Vector3(0,fixedY,0); // rotation matrix y-axis 기반으로 새로운 좌표값 + 기점이 되는 탱크좌표+기존의 카메라 수평 유지 
        LookAt(); //마지막으로 시야큐브를 기점으로 회전시 수평유지 

    }

    private float ReturnRadian(float angle)
    {
        return angle * Mathf.Rad2Deg;
    }

    private float SinTheta(float radian)
    {
        return Mathf.Sin(radian);
    }

    private float CosTheta(float radian)
    {
        return Mathf.Cos(radian);
    }
}
