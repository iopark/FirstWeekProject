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
    /// C#�̱����̱� ������, ����Ƽ�� ������Ʈ�����ΰ� ����϶󱸿� 
    /// </summary>
    private void Awake()
    {
        gameObject.name = "GameManager"; 
        if (instance != null) // ���ʷ� �����ϴ� �༮�̶��, 
        {
            Destroy(this); // Make sure GameManager ������Ʈ�� �� �ϳ��� �����ϰ� �Ѵ�. 
            return;
        }

        instance = this; // �ش� ������Ʈ�� GameManager ������ Instance ��. 
        InitAllManagers();
        DontDestroyOnLoad(this);
         //���� GameManager�� Singleton�̶��, �ش� ��ü�� ���� Singleton�� ���� singleton�̴�.
    }
    //C#�� �ٸ����� �ƹ����� GameManager instance2 = new Gamanager() �̷������� ������ �ν��Ͻ��� ���� '����'�� �����ߴٸ�, 
    // ����Ƽ�� ������Ʈ, GameObj �� �����ִ� �༮�̱⿡, �������䵵 ��¦ �ٸ� �Ӵ���, �����Ҽ��ִ� ����� �ٸ��ڴ�. 

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
            instance = null; // ������ ��ȣ������ static ���� ���ؼ� �������ִ°��� �����ϴ�. 
    }
}
