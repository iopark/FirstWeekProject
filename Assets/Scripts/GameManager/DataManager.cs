using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Game manager�� ����ϰ�, �� ��ü���� ������ �и��� �ϰ�, ������ �ٺ��� �Ǵ� Model - View - Controller �� ������ ���ؼ���, 
    // ��ȣ�ۿ��� ���ΰ� ���������� ���������ʴ���, ��ȣ�ۿ��� �����ϵ��� �߰�ü ������ �ʿ��ϰڴ�. 
    private static DataManager instance;
    public static DataManager Instance
    {
        get { return instance; }
    }
}
