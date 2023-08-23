using System.Collections;
using UnityEngine;

public class PlayerShipCollisionEvent : MonoBehaviour
{
    PlayerControl playerControl;
    PlayerShipParameters shipParameters;

    float loseControllDuration;
    float damageValue;

    Vector3 collisionForce;
    readonly float resistToCollision = 10000f;
    readonly float damageMultipler = 10;


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
        damageValue = colForce/resistToCollision * damageMultipler;
        shipParameters.TakeDamage(damageValue);
    }
}
