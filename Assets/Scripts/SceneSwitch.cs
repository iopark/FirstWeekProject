using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    //probably, 이것또한 Gamemanager로써 제어가 될거 같는 생각이 없지않아 들기는 안하는게 아니긴 하다. 
    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name); // 기본 default 으로 loadscenemode.Single, 하나 로드하고 기존껀 던지는 function 
    }
    public void ChangeSceneByIndex(int index)
    {
        SceneManager.LoadScene(index); 
    }
    public void AddSceneByName(string name) { SceneManager.LoadScene(name, LoadSceneMode.Additive); }

    // 기본적으로 하나씩 Scene을 loadscenemode.single 같은 경우, 로딩이 완료되었을때 구현이 되며 로딩되는동안 정지한다, 커다란 Scene 같은 경우에는 
    // Seemless 를 희망하거나 불필요한 로딩을 줄이는 입장에서는 어려움을 유발할수 있다 
    // 대응방법은 Async: 백그라운드에 로딩이 되며 정지하는상황에 대처할수 있다. 
    //Async 는 조금만더 배우고 도전! 
    //AsyncOperation 말고도 DontDestroyOnLoad(GameObj obj) 로써도 여러가지 씬 동시구현이 가능할것 같다 

}
