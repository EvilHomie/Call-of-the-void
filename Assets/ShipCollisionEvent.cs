using System.Collections;
using UnityEngine;

public class ShipCollisionEvent : MonoBehaviour
{
    PlayerControl playerControl;
    PlayerShipParameters shipParameters;

    float loseControllDuration;
    float damage;
    float damageMod = 10f;

    Vector3 collisionForce;
    readonly float resistToCollision = 100000f;


    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        shipParameters = GetComponent<PlayerShipParameters>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionForce = collision.impulse / Time.fixedDeltaTime;

        LoseControllOnCollision(collisionForce.magnitude);
        TakeDamage(collisionForce.magnitude);
    }

    void LoseControllOnCollision(float colForce)
    {      
        if (playerControl.enabled == true)
        {
            loseControllDuration = colForce / resistToCollision;
            StartCoroutine(DisableController());
        }
        IEnumerator DisableController()
        {
            playerControl.enabled = false;
            yield return new WaitForSeconds(loseControllDuration);
            playerControl.enabled = true;
        }
    }
    

    void TakeDamage(float colForce)
    {
        damage = colForce/resistToCollision * damageMod;
        Debug.Log(damage);
        shipParameters.TakeDamage(damage);
    }
}
