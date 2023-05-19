using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VfxManager : MonoBehaviour
{
    public UnityEvent shell; 

    public void ShellExplode()
    {
        shell?.Invoke();
    }

    private void Start()
    {
        gameObject.name = "VFX Manager";
    }
}
