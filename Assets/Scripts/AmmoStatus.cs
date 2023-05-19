using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoStatus : MonoBehaviour
{
    public TMP_Text ammoStatus;

    private void Awake()
    {
        ammoStatus = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameManager.Data.ShootReact += PrintAmmo;
        GameManager.Data.AmmoStatus += isReloading;
        GameManager.Data.ReloadCounter += PrintReloading; 
    }

    private void OnDisable()
    {
        GameManager.Data.ShootReact -= PrintAmmo;
        GameManager.Data.AmmoStatus -= isReloading;
        GameManager.Data.ReloadCounter -= PrintReloading;
    }

    private void isReloading(bool status)
    {
        if (status)
            ammoStatus.text = "Reload!"; 
    }
    private void PrintAmmo(int current, int max)
    {
        ammoStatus.text = $"{current}/{max}"; 
    }

    private void PrintReloading(int time)
    {
        ammoStatus.text = $"Reloading {time}/3";
    }

}
