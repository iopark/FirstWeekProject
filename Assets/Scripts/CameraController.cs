using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CameraController : MonoBehaviour
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
    private TextMeshProUGUI countText; 
    // Start is called before the first frame update
    void Start()
    {
        //Given 해당 카메라의 position 값과, player의 position값이 있다고 전제를 하는 기능이 있기에, Awake보다는 Start가 적절하다. 
        //cameraPos = transform.position;
        fixedY = transform.position.y;
        offset = -10f; 
        getPointer = player.GetComponentInChildren<SightEdge>(); 
        playerController = player.GetComponentInChildren<TankController>();
        TextMeshProUGUI test = Instantiate(countText, transform.position, transform.rotation);
        transform.parent = test.transform;
        test.transform.localScale = transform.localScale;
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
        // 같은 맥락으로 다른 함수값을 이용해야하는경우에도 lateUpdate()가 필요하겠다. 
        //transform.position = offset + new Vector3(player.transform.position.x, getPointer.transform.position.y, player.transform.position.z);
        //transform.forward = new Vector3(player.transform.position.x, getPointer.transform.position.y, player.transform.position.z); 
        //LookAt(); 

        // 물론 일반 상속값으로 따라가주는것이 용이하겠지만, 
        // 특정 물체의 회전에 따라서 포지션이 변경되야하는 경우에는 삼각함수에 따른 포지션 변화가 필요해 보인다. 
        // 2D Grid로 보았을때에, Camera/ Tank SightCube 는 Tank 를 기점으로 회전값에 따라서 변화하는데, Camera 그리고 SightCube는 서로 대칭하는 관계이다. 
        // 나는 SightCube - Tank 의 회전값을 알고 있기에, 이를 이용해서 새로운 카메라 값을 설정하여주면 되겠다. 

        /* General Idea: (O)Tank를 기점으로, y-axis 축으로 대칭되는 (B)카메라, 그리고 (A)탱크시야점을 찾아야 한다면, 
         * 1. Find vector AO 
         * 2. Normalize AO, (A.x,y,z/vAO) 
         * 3. Utilizing matrix rotation, (after switching angle -> radian), 
         * 4. B-O거리 값과 normalized된 vector 값을 곲하여 vBO 를 찾는다. 
         * 5. BO vector, AO radian 값을 기반으로 y축에 대칭되는 새로운 coordinate 을 생성한다. 
         */

        // 1. Requires deeper understanding in Vectors in general, 
        // 2. Insight in Rotation Matrix, in determining position of new vectors, should there be any updates 

        // But forsake of today's learning, and 유니티 C#기반 프레임워크에서 제공하는 기능들을 이용하는것이 가장 지혜롭겠다. 

        // Utilizing Quaternion.AngleAxis, .LookRotation, .
        // 추가적으로 System.Windows.Media.Media3D 같은 Library 도 존재하겠다. 
        // 뿐만이랴, .normalize로 해당 transform 의 normalized 된 vector값도 바로 반환해준다. 


        float radian = ReturnRadian(rotatingangle); //Angle -> Radian 값 변화 
        Vector3 positionOA = getPointer.transform.position - player.transform.position;
        Vector3 positionOB = transform.position - player.transform.position;
        cameraPos = (positionOA.normalized * offset); // 1,2,3,4 적용 
        //cameraPos.y = fixedY;
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

    public void ChangePlayer(GameObject gameObject)
    {
        player = gameObject;
    }
}
