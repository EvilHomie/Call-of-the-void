using System.Drawing;
using UnityEngine;

public class EnemyWeaponShootInPlayerLogic : MonoBehaviour
{
    Transform firePoint;
    IWeapon weapon;    
    public float weaponDistance;
    public bool targetInFocus;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();
        firePoint = transform.Find("Pivot Point").transform.Find("Fire Point");
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit raycastHit, weaponDistance, 1))
        {
            GameObject target = raycastHit.collider.transform.root.gameObject;
            if (target.CompareTag("Player") || target.CompareTag("Asteroid") || target.CompareTag("Resource"))
            {
                targetInFocus = true;
                weapon.Shoot();                
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

        Debug.DrawRay(firePoint.position, firePoint.forward * weaponDistance);
    }
}
