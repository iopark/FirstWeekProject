using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    // Game manager�� ����ϰ�, �� ��ü���� ������ �и��� �ϰ�, ������ �ٺ��� �Ǵ� Model - View - Controller �� ������ ���ؼ���, 
    // ��ȣ�ۿ��� ���ΰ� ���������� ���������ʴ���, ��ȣ�ۿ��� �����ϵ��� �߰�ü ������ �ʿ��ϰڴ�. 
    private static DataManager instance;

    [Header("Tank Ammo")]
    [SerializeField] private int shootCount;
    [SerializeField] private int reloadCount;
    [SerializeField] private bool reloadStatus; 

    public UnityAction<int, int> ShootReact;
    public UnityAction<int> ReloadCounter;
    public UnityAction<bool> AmmoStatus;
    public static DataManager Instance
    {
        get { return instance; }
    }
    public void ShootCount(int roundCount)
    {
        shootCount = roundCount;
        ShootReact?.Invoke(shootCount); 
    }

    public void ReloadCount(int second)
    {
        reloadCount = second;
        ReloadCounter?.Invoke(second);
    }

    public void ReloadStatus(bool status)
    {
        reloadStatus = status;
        AmmoStatus?.Invoke(status);
    }

    private void Start()
    {
        gameObject.name = "Data Manager";
    }
}
