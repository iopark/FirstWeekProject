using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankShooter : MonoBehaviour
{
    [Header("Tank Related")]
    [SerializeField] private Transform muzzlePointer;

    [Header("Ammo Related")]
    public Bullet bulletPrefab;
    [SerializeField] private int bulletCount;
    [SerializeField] private int bulletLimit;
    [SerializeField] private bool reloading;
    [Range(0f,20f)] private float repeatTime; // ���� interval �������ֱ� 

    private void Start()
    { 
        muzzlePointer = GetComponent<Transform>();
        bulletPrefab = GetComponent<Bullet>();
        bulletLimit = 20;
        bulletCount = bulletLimit; 
        reloading = false;
        repeatTime = 0.5f; 
    }
    private Coroutine bulletRoutine;

    private void Shoot()
    {
        Instantiate(bulletPrefab, muzzlePointer.transform.position, muzzlePointer.transform.rotation);
        bulletCount--;
    }
    private void OnFire(InputValue value)
    {
        if (reloading) // �������� �����϶��� �߻簡 �Ǹ� �ʹ� ���������
        {
            //���� ������� ����ó�� 
            //AmmoStatus.text = "Reloading";
            return;
        }
        Debug.Log("Fire");
        //GameObject obj =  Instantiate(bulletPrefab); // GameObject.Function: Instantiate the targeting object 
        //obj.transform.position = transform.position;
        //obj.transform.rotation = transform.rotation;
        if (bulletCount <= 0)
        {
            Debug.Log("Ran out of Ammo");
            //AmmoStatus.text = "Reload!"; Replacing with UnityEvent, Conducting implementation of MVC DP
            return;
        }
        Shoot();
        //--bulletCount;
        //Instantiate(bulletPrefab, muzzlePointer.transform.position, muzzlePointer.transform.rotation);
        //SetText();
    }
    private void OnRepeatFire(InputValue value)
    {
        if (value.isPressed && !reloading) // ������� �������̶�� ����Ǹ� �ʹ� ��������� 
        {
            Debug.Log("button Pressed"); // Here, implement premade coroutine for the continuous fire 
            bulletRoutine = StartCoroutine(BulletMakeRoutine());
            // instance is saved, so it could be called back for stopping 
        }
        else
        {
            StopCoroutine(bulletRoutine); // Stop the saved instance of the coroutine 
            Debug.Log("Button letgo"); // �۾����� �׽�Ʈ/���� �׽�Ʈ�ϱ����� �ּ����� �� �ʿ��� �۾� 
        }
    }
    IEnumerator BulletMakeRoutine()
    {
        while (bulletCount > 0 && !reloading)
        {
            Shoot();
            //Instantiate(bulletPrefab, muzzlePointer.transform.position, muzzlePointer.transform.rotation);
            ////�ν��Ͻÿ���Ʈ: (������ �ν���Ʈ, #�����ε���׵�)�� ���ؼ� �ش���� ��� ������ �����ϰ� ���ִ� GameObject Function �̴�. 
            //--bulletCount;
            //SetText();
            yield return new WaitForSeconds(repeatTime);
        }
        if (reloading)
            //AmmoStatus.text = "Reloading!";
        //AmmoStatus.text = "Reload!";
        Debug.Log("Continuous Fire stopped: Ran out of Ammo");
    }

    /// <summary>
    /// ������ ���� �ڷ�ƾ. �����ӽð��� ������ 3�ʵ��� �����Ѵ�. 
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletReload()
    {
        //Reload takes 3 seconds. 
        int count = 1;
        while (count < 4 & !reloading)
        {
            reloading = true;
            //AmmoStatus.text = $"Reloading Rounds : {count} /3";
            count++;
            yield return new WaitForSeconds(1); //1�ʸ� ������ �ش� Ienumerator/ for loop MoveNext(), Current()�� �ݺ�ȣ���Ѵ�. Frame ���̿��� �����ϰ�. 
        }
 
        reloading = false; 
        bulletCount = bulletLimit;
        //SetText();
        Debug.Log("Reload End");
    }
    private void SetText() // �̰Ͷ��� MVC �� �ǰ��ؼ� �и��۾��� �ǽ��Ҽ� �ִ�: this case, UnityEvent ����Ͽ�
    {
        //AmmoStatus.text = $"{bulletCount.ToString()}/{bulletLimit.ToString()}\t ";

    }
    private void OnReload(InputValue value)
    {
        //Reload Ű�� �Էµɶ����� �����Ѵ� -> �ڷ�ƾ�� 
        Debug.Log("Reload Start");
        StartCoroutine(BulletReload());
    }
}
