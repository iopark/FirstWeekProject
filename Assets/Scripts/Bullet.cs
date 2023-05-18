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
        rigidbody.velocity = transform.forward * bulletSpeed; // �ش� gameobj �� �ٶ󺸰��ִ¹����� �������� �����Ͽ� �ش�.
        // Rigidbody.velocity �� rigidbody �� �ӵ��� �����Ͽ� �ش�. 
        Destroy(gameObject, 5f);  // destroy this obj 5seconds after initializing 
    }

}
