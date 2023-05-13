using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Component getPointer; 
    // Start is called before the first frame update
    void Start()
    {
        //Given �ش� ī�޶��� position ����, player�� position���� �ִٰ� ������ �ϴ� ����� �ֱ⿡, Awake���ٴ� Start�� �����ϴ�. 

        offset = transform.position - player.transform.position;
        getPointer = player.GetComponentInChildren<SightEdge>(); 
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
        transform.position = offset + player.transform.position;
        //transform.forward = new Vector3(0, player.transform.position.y, player.transform.forward.z); 
        LookAt(); 
    }
}
