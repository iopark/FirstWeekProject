using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField] private float bulletSpeed; 
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.velocity = transform.forward * bulletSpeed; // 해당 gameobj 의 바라보고있는방향을 기준으로 설정하여 준다.
        // Rigidbody.velocity 로 rigidbody 의 속도를 설정하여 준다. 
        Destroy(gameObject, 5f);  // destroy this obj 5seconds after initializing 
    }

}
