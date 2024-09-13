using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public int currentAmmo = 0;

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        Debug.Log("Ammo added. Current ammo: " + currentAmmo);
    }
}
