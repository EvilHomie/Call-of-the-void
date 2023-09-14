using UnityEngine;

public class CheckPlayerInFocus : MonoBehaviour
{
    [SerializeField] float turretShootDistance;
    [SerializeField] Transform firePoint;
    IWeapon weapon;
    public bool targetInFocus;

    private void Awake()
    {
        weapon = GetComponent<IWeapon>();
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit raycastHit, turretShootDistance))
        {
            if (raycastHit.collider.gameObject.CompareTag("Player"))
            {
                weapon.Shoot();
                targetInFocus = true;
            }
            else { targetInFocus = false; }

            if (raycastHit.collider.gameObject.CompareTag("Asteroid"))
            {
                weapon.Shoot();
            }
        }
        else { targetInFocus = false; }
    }
}
