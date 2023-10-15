using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    List <IWeapon> weaponsScripts = new();
    [SerializeField] GameObject[] weapons;

    void Start()
    {
        FillList();
        weapons[0].SetActive(false);
    }

    void FillList()
    {
        foreach (var weapon in weapons)
        {
            weaponsScripts.Add(weapon.GetComponent<IWeapon>());
        }
    }

    void Update()
    {
        Shoot();
        ChangeWeapon();
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].activeSelf)
                {
                    weaponsScripts[i].Shoot();
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].activeSelf)
                {
                    weaponsScripts[i].Stop();
                }
            }
        }
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(true);
        }
    }
}
