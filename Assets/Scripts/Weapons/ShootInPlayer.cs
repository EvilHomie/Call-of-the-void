using UnityEngine;

public class ShootInPlayer : MonoBehaviour
{
    Transform firePoint;
    IWeapon weapon;
    float weaponDistance = 100;
    public bool targetInFocus;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();
        firePoint = transform.Find("Pivot Point").Find("Fire Point");
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit raycastHit, weaponDistance, 1))
        {
            GameObject target = raycastHit.collider.gameObject;
            if (target.CompareTag("Player") || target.CompareTag("Asteroid") || target.CompareTag("Resource"))
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
