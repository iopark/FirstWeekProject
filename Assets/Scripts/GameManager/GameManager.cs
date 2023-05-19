using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Yeaaaap Babeee
    // Singletooooon babeeeeeeeeeee 
    private static GameManager instance;
    private static DataManager dataManager;
    private static VfxManager vfxManager;
    public static GameManager Instance
    {
        get { return instance; }
    }
    public static DataManager Data
    {
        get { return dataManager; }
    }
    public static VfxManager Vfx
    {
        get { return vfxManager;}
    }

    /// <summary>
    /// C#싱글턴이긴 하지만, 유니티의 컴포넌트형태인걸 기억하라구우 
    /// </summary>
    private void Awake()
    {
        gameObject.name = "GameManager"; 
        if (instance != null) // 최초로 생성하는 녀석이라면, 
        {
            Destroy(this); // Make sure GameManager 컴포넌트는 단 하나만 존재하게 한다. 
            return;
        }

        instance = this; // 해당 컴포넌트가 GameManager 유일한 Instance 다. 
        InitAllManagers();
        DontDestroyOnLoad(this);
         //만약 GameManager가 Singleton이라면, 해당 객체가 지닌 Singleton은 전부 singleton이다.
    }
    //C#과 다른점은 아무래도 GameManager instance2 = new Gamanager() 이런식으로 유일한 인스턴스에 대해 '참조'가 가능했다면, 
    // 유니티는 컴포넌트, GameObj 를 끼고있는 녀석이기에, 생성개념도 살짝 다를 뿐더러, 참조할수있는 방법도 다르겠다. 

    private void InitAllManagers()
    {
        GameObject dataObj = new GameObject() { name = "DataManager" };
        dataObj.transform.SetParent(transform);
        dataManager = dataObj.AddComponent<DataManager>();

        GameObject vfxObj = new GameObject() { name = "VFXManager" };
        vfxObj.transform.SetParent(transform);
        vfxManager = vfxObj.AddComponent<VfxManager>();
    }
    private void OnDestroy()
    {
        if (instance != null)
            instance = null; // 아직은 모호하지만 static 값에 대해서 정리해주는것이 유익하다. 
    }
}
