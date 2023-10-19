using UnityEngine;

public class ShootInPlayer : MonoBehaviour
{
    IWeapon weapon;
    public float weaponDistance;
    public bool targetInFocus;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, weaponDistance, 1))
        {
            GameObject target = raycastHit.collider.transform.root.gameObject;
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
