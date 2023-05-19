using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    //probably, �̰Ͷ��� Gamemanager�ν� ��� �ɰ� ���� ������ �����ʾ� ���� ���ϴ°� �ƴϱ� �ϴ�. 
    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name); // �⺻ default ���� loadscenemode.Single, �ϳ� �ε��ϰ� ������ ������ function 
    }
    public void ChangeSceneByIndex(int index)
    {
        SceneManager.LoadScene(index); 
    }
    public void AddSceneByName(string name) { SceneManager.LoadScene(name, LoadSceneMode.Additive); }

    // �⺻������ �ϳ��� Scene�� loadscenemode.single ���� ���, �ε��� �Ϸ�Ǿ����� ������ �Ǹ� �ε��Ǵµ��� �����Ѵ�, Ŀ�ٶ� Scene ���� ��쿡�� 
    // Seemless �� ����ϰų� ���ʿ��� �ε��� ���̴� ���忡���� ������� �����Ҽ� �ִ� 
    // ��������� Async: ��׶��忡 �ε��� �Ǹ� �����ϴ»�Ȳ�� ��ó�Ҽ� �ִ�. 
    //Async �� ���ݸ��� ���� ����! 
    //AsyncOperation ���� DontDestroyOnLoad(GameObj obj) �νᵵ �������� �� ���ñ����� �����Ұ� ���� 

}
