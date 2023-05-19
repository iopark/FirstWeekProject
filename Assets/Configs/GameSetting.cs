using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSetting : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // �̷������� �ε��ϱ��� �����ؾ��� �Լ���ҵ��� �����Ҽ� �ִ�! 
    private static void Init()
    {
        GameSettings();
    }

    private static void GameSettings()
    {
        if (GameManager.Instance == null) 
        {
            GameObject GM = new GameObject(); 
            GM.AddComponent<GameManager>();
        }
    }
}
