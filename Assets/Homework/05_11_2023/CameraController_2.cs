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
        //Given 해당 카메라의 position 값과, player의 position값이 있다고 전제를 하는 기능이 있기에, Awake보다는 Start가 적절하다. 

        offset = transform.position - player.transform.position;
    }

    // Update 는 비록 새로운 프레임 이전에 호출되지만, Delegate += function 때와 비슷하게 호출순서를 보장하지는 못한다. 
    // 특히 Camera offset 값에 따라 카메라의 위치가 결정될때, 일반적인 물리연산함수이후에 Update()가 되어야 하는데, 이는 보장되지 않는다. 
    // 따라서 해당scene 의 모든 Game object 의 메세지함수중, Update() 가 다 끝난 이후에 진행되는 FixedUpdate()를 사용하는것이 일반적이겠다. 
    void Update()
    {
        
    }

    private void LateUpdate()
    {
       // 같은 맥락으로 다른 함수값을 (프레임 기준으로) 이용해야하는경우에도 lateUpdate()가 필요하겠다.
       transform.position = offset + player.transform.position;
    }
}
