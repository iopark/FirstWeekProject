using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_2 : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset; 
    // Start is called before the first frame update
    void Start()
    {
        //Given �ش� ī�޶��� position ����, player�� position���� �ִٰ� ������ �ϴ� ����� �ֱ⿡, Awake���ٴ� Start�� �����ϴ�. 

        offset = transform.position - player.transform.position;
    }

    // Update �� ��� ���ο� ������ ������ ȣ�������, Delegate += function ó�� ������ ���������� ���Ѵ�. 
    // Ư�� Camera offset ���� ���� ī�޶��� ��ġ�� �����ɶ�, �Ϲ����� ���������Լ����Ŀ� Update()�� �Ǿ�� �ϴµ�, �̸� �������� ���Ѵ�. 
    // ���� �ش�scene �� ��� Game object �� �޼����Լ���, Update() �� �� ���� ���Ŀ� ����Ǵ� FixedUpdate()�� ����ϴ°��� �Ϲ����̰ڴ�. 
    void Update()
    {
        
    }

    private void LateUpdate()
    {
       // ���� �ƶ����� �ٸ� �Լ����� �̿��ؾ��ϴ°�쿡�� lateUpdate()�� �ʿ��ϰڴ�. 
       transform.position = offset + player.transform.position;
    }
}
