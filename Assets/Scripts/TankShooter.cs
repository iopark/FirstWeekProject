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
    [Range(0f,20f)] private float repeatTime; // 연사 interval 설정해주기 

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
        if (reloading) // 장전중인 상태일때는 발사가 되면 너무 사기임으로
        {
            //가드 방식으로 예외처리 
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
        if (value.isPressed && !reloading) // 연사또한 장전중이라면 진행되면 너무 사기임으로 
        {
            Debug.Log("button Pressed"); // Here, implement premade coroutine for the continuous fire 
            bulletRoutine = StartCoroutine(BulletMakeRoutine());
            // instance is saved, so it could be called back for stopping 
        }
        else
        {
            StopCoroutine(bulletRoutine); // Stop the saved instance of the coroutine 
            Debug.Log("Button letgo"); // 작업도중 테스트/반응 테스트하기위한 최소한의 꼭 필요한 작업 
        }
    }
    IEnumerator BulletMakeRoutine()
    {
        while (bulletCount > 0 && !reloading)
        {
            Shoot();
            //Instantiate(bulletPrefab, muzzlePointer.transform.position, muzzlePointer.transform.rotation);
            ////인스턴시에이트: (생성전 인스턴트, #오버로드사항들)을 통해서 해당씬에 즉시 생성이 가능하게 해주는 GameObject Function 이다. 
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
    /// 장전을 위한 코루틴. 프레임시간과 별개로 3초동안 시행한다. 
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
            yield return new WaitForSeconds(1); //1초를 텀으로 해당 Ienumerator/ for loop MoveNext(), Current()를 반복호출한다. Frame 사이에서 교묘하게. 
        }
 
        reloading = false; 
        bulletCount = bulletLimit;
        //SetText();
        Debug.Log("Reload End");
    }
    private void SetText() // 이것또한 MVC 에 의거해서 분리작업을 실시할수 있다: this case, UnityEvent 사용하여
    {
        //AmmoStatus.text = $"{bulletCount.ToString()}/{bulletLimit.ToString()}\t ";

    }
    private void OnReload(InputValue value)
    {
        //Reload 키가 입력될때마다 실행한다 -> 코루틴을 
        Debug.Log("Reload Start");
        StartCoroutine(BulletReload());
    }
}
