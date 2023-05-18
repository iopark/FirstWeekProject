using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Game manager와 비슷하게, 각 객체별로 역할을 분명히 하고, 게임의 근본이 되는 Model - View - Controller 를 익히기 위해서는, 
    // 상호작용을 서로가 직접적으로 참조하지않더라도, 상호작용이 가능하도록 중간체 역할이 필요하겠다. 
    private static DataManager instance;
    public static DataManager Instance
    {
        get { return instance; }
    }
}
