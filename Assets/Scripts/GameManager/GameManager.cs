using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Yeaaaap Babeee
    // Singletooooon babeeeeeeeeeee 
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// C#�̱����̱� ������, ����Ƽ�� ������Ʈ�����ΰ� ����϶󱸿� 
    /// </summary>
    private void Awake()
    {
        if (instance == null) // ���ʷ� �����ϴ� �༮�̶��, 
        {
            instance = this; // �ش� ������Ʈ�� GameManager ������ Instance ��. 
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this); // Make sure GameManager ������Ʈ�� �� �ϳ��� �����ϰ� �Ѵ�. 
    }
    //C#�� �ٸ����� �ƹ����� GameManager instance2 = new Gamanager() �̷������� ������ �ν��Ͻ��� ���� '����'�� �����ߴٸ�, 
    // ����Ƽ�� ������Ʈ, GameObj �� �����ִ� �༮�̱⿡, �������䵵 ��¦ �ٸ� �Ӵ���, �����Ҽ��ִ� ����� �ٸ��ڴ�. 

    private void OnDestroy()
    {
        if (instance != null)
            instance = null; // ������ ��ȣ������ static ���� ���ؼ� �������ִ°��� �����ϴ�. 
    }
}
