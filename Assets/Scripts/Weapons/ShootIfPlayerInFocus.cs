using UnityEngine;

public class ShootIfPlayerInFocus : MonoBehaviour
{
    Transform firePoint;
    IWeapon weapon;
    float weaponCheckDistance = 100;
    public bool targetInFocus;
    public bool shootIsStop;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();
        firePoint = transform.Find("Pivot Point").Find("Fire Point");
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit raycastHit, weaponCheckDistance, 1))
        {
            if (raycastHit.collider.gameObject.CompareTag("Player"))
            {
                weapon.Shoot();
                targetInFocus = true;
            }
            else if (raycastHit.collider.gameObject.CompareTag("Asteroid"))
            {
                weapon.Shoot();
                targetInFocus = true;
            }
            else
            {
                if (targetInFocus)
                {
                    targetInFocus = false;
                    weapon.Stop();
                }                
            }
        }
        else
        {
            if (targetInFocus)
            {
                targetInFocus = false;
                weapon.Stop();
            }  
        }
    }
}
