using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCoroutine : MonoBehaviour
{
    private void Start()
    {
        coroutine= StartCoroutine(SubRoutine()); 
    }
    private Coroutine coroutine; 

    private void CoroutineStart()
    {

    }

    private void CoroutineStop()
    {
        StopCoroutine(coroutine);
        StopAllCoroutines(); 
    }
    IEnumerator SubRoutine()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log($"{i}�� ����");
            //yield return null; // ������ ���°�� 
            yield return new WaitForSeconds(1); 
        }
    }
}
