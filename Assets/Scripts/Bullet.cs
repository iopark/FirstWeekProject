using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // 이렇게 Rigidbody가 없으면 안되는 상황을 attribute 로 통제가 가능하다. 
public class Bullet : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float bulletSpeed;

    [Header("Effect Region")]
    public GameObject explosive; 

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //GameManager.Vfx.shell += BulletEffect;
        rigidbody.velocity = transform.forward * bulletSpeed; // 해당 gameobj 의 바라보고있는방향을 기준으로 설정하여 준다.
        // Rigidbody.velocity 로 rigidbody 의 속도를 설정하여 준다. 
        Destroy(gameObject, 5f);  // destroy this obj 5seconds after initializing 
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); 
    }
    private void OnDestroy()
    {
        BulletEffect();
        //Instantiate(explosive, transform.position, transform.rotation);
        //GameManager.Vfx.ShellExplode(); //임시로 bulletefffect가 다른곳에 있다고 가정하여 설정 
        // 아마 다른객체에 있다면 입력 parameter 로 Vector3 나 Tranform을 설정해주면 그자리에 effect가 넣어줄수있게 설정 가능할것으로 예상 
        //GameManager.Vfx.shell -= BulletEffect;
    }
    /// <summary>
    /// 이것또한 분리가 가능하겠다.
    /// 당분간은 같이 가며, vfxmanager의 중간체에 의거해 실행되게 한다. 
    /// </summary>
    private void BulletEffect()
    {
        Instantiate(explosive, transform.position, transform.rotation);
    }
}
