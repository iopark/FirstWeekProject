using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSetting : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // 이런식으로 로딩하기전 구동해야할 함수요소들을 설정할수 있다! 
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
