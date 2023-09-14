using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    IWeapon[] weapons;

    void Awake()
    {
        FillList();
    }

    void FillList()
    {
        weapons = GetComponentsInChildren<IWeapon>();
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    foreach (var weapon in weapons) { weapon.Shoot(); }
        //}

        if (Input.GetButton("Fire1"))
        {
            foreach (var weapon in weapons) { weapon.Shoot(); }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            foreach (var weapon in weapons) { weapon.Stop(); }
        }
    }
}
