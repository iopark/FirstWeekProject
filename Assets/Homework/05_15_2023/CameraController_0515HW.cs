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
    private TextMeshProUGUI countText; //��ũ�� ��ź ������ ���� ���� ui ���� 
    // Start is called before the first frame update
    void Start()
    {
        //Given �ش� ī�޶��� position ����, player�� position���� �ִٰ� ������ �ϴ� ����� �ֱ⿡, Awake���ٴ� Start�� �����ϴ�. 
        //cameraPos = transform.position;
        fixedY = transform.position.y;
        offset = -10f; 
        getPointer = player.GetComponentInChildren<SightEdge>(); 
        playerController = player.GetComponentInChildren<TankController>();
        TextMeshProUGUI test = Instantiate(countText, transform.position, transform.rotation); // �ش� TMPro ����
        transform.parent = test.transform; // �θ� tranform �������־ 
        test.transform.localScale = transform.localScale;  // text ���� ũ�� ���� 
    }

    // Update �� ��� ���ο� ������ ������ ȣ�������, Delegate += function ó�� ������ ���������� ���Ѵ�. 
    // Ư�� Camera offset ���� ���� ī�޶��� ��ġ�� �����ɶ�, �Ϲ����� ���������Լ����Ŀ� Update()�� �Ǿ�� �ϴµ�, �̸� �������� ���Ѵ�. 
    // ���� �ش�scene �� ��� Game object �� �޼����Լ���, Update() �� �� ���� ���Ŀ� ����Ǵ� FixedUpdate()�� ����ϴ°��� �Ϲ����̰ڴ�. 
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
        cameraPos = (positionOA.normalized * offset); // 1,2,3,4 ���� 
        //cameraPos.y = fixedY;
        //�ֱٵ�� Unity�� Radian�� ���� Quaternion.EulerAngle�� ����ż�����ϸ� Euler�� ����϶�� ��ǻ� ���а� �ִ�. 
        cameraPos = Quaternion.AngleAxis(playerController.rotatingAngle, player.transform.up) * cameraPos; // ī�޶�-��ũ-�þ� ��Ī���� ���� = ��ũ-�þ� ȸ������ �ݴ�Ǵ� ��ǥ * ī�޶�-��ũ �������Ͱ�
        transform.position = cameraPos + player.transform.position + new Vector3(0,fixedY,0); // rotation matrix y-axis ������� ���ο� ��ǥ�� + ������ �Ǵ� ��ũ��ǥ+������ ī�޶� ���� ���� 
        LookAt(); //���������� �þ�ť�긦 �������� ȸ���� �������� 

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
