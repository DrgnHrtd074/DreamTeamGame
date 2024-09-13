using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text ammoText;
    private object ammoInventory;

    private void Start()
    {
        ammoInventory = FindAnyObjectByType<AmmoManager>();
        if (ammoInventory == null)
        {
            Debug.LogError("AmmoInventory component not found!");
        }

        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(ammoInventory != null)
        {
            UpdateAmmoUI();
        }
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + ammoInventory.ToString();
    }
}
