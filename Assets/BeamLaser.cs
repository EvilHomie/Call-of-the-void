using UnityEngine;

public class BeamLaser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] float playerAngle;

    readonly float beamLength = 200f;

    private void Start()
    {
        lineRenderer.enabled = false;
    }
    public void EnableLaser(Vector3 mousePosition)
    {
        lineRenderer.enabled = true;
    }

    public void UpdateLaser(Vector3 mousePosition)
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer. SetPosition(1, firePoint.position + firePoint.forward * beamLength);
    }

    public void DisableLaser(Vector3 mousePosition)
    {
        lineRenderer.enabled = false;
    }
   

    //void WithMathfAtan2(Vector3 mousePosition)
    //{
    //    Vector3 direction = mousePosition - firePoint.position;
    //    firePointAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    //    firePointAngle = (firePointAngle + 360) % 360;    
    //    float parentAngle = transform.parent.transform.eulerAngles.y;
    //    firePointAngle = Mathf.Clamp(firePointAngle, parentAngle - 90, parentAngle + 90);
    //    transform.localRotation = Quaternion.AngleAxis(firePointAngle, Vector3.up);
    //}

    //void WithRotateTowards(Vector3 mousePosition)
    //{
    //    Vector3 mouseDirection = mousePosition - firePoint.position;
    //    Vector3 mouseDirection2D = new Vector3(mouseDirection.x, 0, mouseDirection.z);
    //    Quaternion toMouse = Quaternion.LookRotation(mouseDirection2D, Vector3.up);
    //    float parentAngle = transform.parent.transform.eulerAngles.y;
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toMouse, firePointRotateSpeed * Time.deltaTime);
    //    transform.eulerAngles = new Vector3(0, Mathf.Clamp(transform.rotation.eulerAngles.y, parentAngle - 45f, parentAngle + 45f), 0);
    //}
}

