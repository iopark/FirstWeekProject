using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // �̷��� Rigidbody�� ������ �ȵǴ� ��Ȳ�� attribute �� ������ �����ϴ�. 
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
        rigidbody.velocity = transform.forward * bulletSpeed; // �ش� gameobj �� �ٶ󺸰��ִ¹����� �������� �����Ͽ� �ش�.
        // Rigidbody.velocity �� rigidbody �� �ӵ��� �����Ͽ� �ش�. 
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
        //GameManager.Vfx.ShellExplode(); //�ӽ÷� bulletefffect�� �ٸ����� �ִٰ� �����Ͽ� ���� 
        // �Ƹ� �ٸ���ü�� �ִٸ� �Է� parameter �� Vector3 �� Tranform�� �������ָ� ���ڸ��� effect�� �־��ټ��ְ� ���� �����Ұ����� ���� 
        //GameManager.Vfx.shell -= BulletEffect;
    }
    /// <summary>
    /// �̰Ͷ��� �и��� �����ϰڴ�.
    /// ��а��� ���� ����, vfxmanager�� �߰�ü�� �ǰ��� ����ǰ� �Ѵ�. 
    /// </summary>
    private void BulletEffect()
    {
        Instantiate(explosive, transform.position, transform.rotation);
    }
}
