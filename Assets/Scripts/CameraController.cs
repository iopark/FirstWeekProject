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
        //Given �ش� ī�޶��� position ����, player�� position���� �ִٰ� ������ �ϴ� ����� �ֱ⿡, Awake���ٴ� Start�� �����ϴ�. 
        //cameraPos = transform.position;
        fixedY = transform.position.y;
        offset = -10f; 
        getPointer = player.GetComponentInChildren<SightEdge>(); 
        playerController = player.GetComponentInChildren<TankController>();
        TextMeshProUGUI test = Instantiate(countText, transform.position, transform.rotation);
        transform.parent = test.transform;
        test.transform.localScale = transform.localScale;
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
        // ���� �ƶ����� �ٸ� �Լ����� �̿��ؾ��ϴ°�쿡�� lateUpdate()�� �ʿ��ϰڴ�. 
        //transform.position = offset + new Vector3(player.transform.position.x, getPointer.transform.position.y, player.transform.position.z);
        //transform.forward = new Vector3(player.transform.position.x, getPointer.transform.position.y, player.transform.position.z); 
        //LookAt(); 

        // ���� �Ϲ� ��Ӱ����� �����ִ°��� �����ϰ�����, 
        // Ư�� ��ü�� ȸ���� ���� �������� ����Ǿ��ϴ� ��쿡�� �ﰢ�Լ��� ���� ������ ��ȭ�� �ʿ��� ���δ�. 
        // 2D Grid�� ����������, Camera/ Tank SightCube �� Tank �� �������� ȸ������ ���� ��ȭ�ϴµ�, Camera �׸��� SightCube�� ���� ��Ī�ϴ� �����̴�. 
        // ���� SightCube - Tank �� ȸ������ �˰� �ֱ⿡, �̸� �̿��ؼ� ���ο� ī�޶� ���� �����Ͽ��ָ� �ǰڴ�. 

        /* General Idea: (O)Tank�� ��������, y-axis ������ ��Ī�Ǵ� (B)ī�޶�, �׸��� (A)��ũ�þ����� ã�ƾ� �Ѵٸ�, 
         * 1. Find vector AO 
         * 2. Normalize AO, (A.x,y,z/vAO) 
         * 3. Utilizing matrix rotation, (after switching angle -> radian), 
         * 4. B-O�Ÿ� ���� normalized�� vector ���� ���Ͽ� vBO �� ã�´�. 
         * 5. BO vector, AO radian ���� ������� y�࿡ ��Ī�Ǵ� ���ο� coordinate �� �����Ѵ�. 
         */

        // 1. Requires deeper understanding in Vectors in general, 
        // 2. Insight in Rotation Matrix, in determining position of new vectors, should there be any updates 

        // But forsake of today's learning, and ����Ƽ C#��� �����ӿ�ũ���� �����ϴ� ��ɵ��� �̿��ϴ°��� ���� �����Ӱڴ�. 

        // Utilizing Quaternion.AngleAxis, .LookRotation, .
        // �߰������� System.Windows.Media.Media3D ���� Library �� �����ϰڴ�. 
        // �Ӹ��̷�, .normalize�� �ش� transform �� normalized �� vector���� �ٷ� ��ȯ���ش�. 


        float radian = ReturnRadian(rotatingangle); //Angle -> Radian �� ��ȭ 
        Vector3 positionOA = getPointer.transform.position - player.transform.position;
        Vector3 positionOB = transform.position - player.transform.position;
        cameraPos = (positionOA.normalized * offset); // 1,2,3,4 ���� 
        //cameraPos.y = fixedY;
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

    public void ChangePlayer(GameObject gameObject)
    {
        player = gameObject;
    }
}
