using System.Collections;
using UnityEngine;

public class LoseControllOnCollision : MonoBehaviour
{
    PlayerControl playerControl;

    float loseControllDuration;

    Vector3 collisionForce;
    readonly float collisionResist = 500f;


    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.GetComponent<ProjectileManager>())
        {
            collisionForce = collision.impulse;

            LoseControll(collisionForce.magnitude);
        }                 
    }

    void LoseControll(float colForce)
    {      
        if (playerControl.enabled)
        {
            loseControllDuration = colForce / collisionResist;
            StartCoroutine(DisableController());
        }
        IEnumerator DisableController()
        {
            playerControl.enabled = false;
            yield return new WaitForSeconds(loseControllDuration);
            playerControl.enabled = true;
        }
    }    
}
