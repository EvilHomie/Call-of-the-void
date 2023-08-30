using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RCSSystemManager : MonoBehaviour
{
    [SerializeField] ParticleSystem rCS_FL;
    [SerializeField] ParticleSystem rCS_FR;
    [SerializeField] ParticleSystem rCS_BL;
    [SerializeField] ParticleSystem rCS_BR;
    [SerializeField] ParticleSystem rCS_BLF;
    [SerializeField] ParticleSystem rCS_BRF;
    [SerializeField] ParticleSystem rCS_BLB;
    [SerializeField] ParticleSystem rCS_BRB;

    Rigidbody rootRB;
    Vector3 oldVelocity;
    Vector3 oldPos;
    Vector3 currentVelocity;

    float rCSPrecision = 2;

    bool movingLeft = false;
    bool leftRCSEnabled = false;

    bool movingRight = false;
    bool rightRCSEnabled = false;

    bool movingForward = false;
    bool forwardRCSEnabled = false;

    bool movingBack = false;
    bool backRCSEnabled = false;

    private void Start()
    {
        rootRB = transform.root.GetComponent<Rigidbody>();
        oldVelocity = transform.InverseTransformDirection(rootRB.velocity);
        oldPos = transform.InverseTransformDirection(transform.root.position);

    }

    private void FixedUpdate()
    {
        RCSLogic();
    }        

    void RCSLogic()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rootRB.velocity);


        LeftRCS(localVelocity);
        RightRCS(localVelocity);
        FrontRCS(localVelocity);
        BackRCS(localVelocity);

        //Debug.Log(localVelocity.x + "     " + localVelocity.magnitude);
        oldVelocity.x = localVelocity.x;
        oldVelocity.z = localVelocity.z;
    }

    void LeftRCS(Vector3 velocity)
    {
        float speedDifX = Mathf.Abs(velocity.x) - Mathf.Abs(oldVelocity.x);
        
        if ( !movingLeft && velocity.x < -rCSPrecision && speedDifX > 0)
        {
            movingLeft = true;
            leftRCSEnabled = false;
            rCS_FL.Stop();
            rCS_BL.Stop();
            Debug.Log("Moveleft");
        }
        else if (movingLeft && !leftRCSEnabled && speedDifX < 0)
        {
            movingLeft = false;
            leftRCSEnabled = true;
            rCS_FL.Play();
            rCS_BL.Play();
            Debug.Log("StopMoveLeft");
        }
        else if (velocity.x > -rCSPrecision && velocity.x < rCSPrecision && leftRCSEnabled)
        {
            leftRCSEnabled = false;
            rCS_FL.Stop();
            rCS_BL.Stop();
            Debug.Log("RCSDisable");
        }
        
    }

    void RightRCS(Vector3 velocity)
    {
        float speedDifX = Mathf.Abs(velocity.x) - Mathf.Abs(oldVelocity.x);

        if (!movingRight && velocity.x > rCSPrecision && speedDifX > 0)
        {
            movingRight = true;
            rightRCSEnabled = false;
            rCS_FR.Stop();
            rCS_BR.Stop();
            Debug.Log("MoveRight");
        }
        else if (movingRight && !rightRCSEnabled && speedDifX < 0)
        {
            movingRight = false;
            rightRCSEnabled = true;
            rCS_FR.Play();
            rCS_BR.Play();
            Debug.Log("StopMoveRight");
        }
        else if (velocity.x > -rCSPrecision && velocity.x < rCSPrecision && rightRCSEnabled)
        {
            rightRCSEnabled = false;
            rCS_FR.Stop();
            rCS_BR.Stop();
            Debug.Log("RCSDisable");
        }
    }

    void FrontRCS(Vector3 velocity)
    {
        float speedDifZ = Mathf.Abs(velocity.z) - Mathf.Abs(oldVelocity.z);
        

        if (!movingForward && velocity.z > rCSPrecision && speedDifZ > 0)
        {
            movingForward = true;
            forwardRCSEnabled = false;
            rCS_BLF.Stop();
            rCS_BRF.Stop();
            Debug.Log("MoveForward");
        }
        else if (movingForward && !forwardRCSEnabled && speedDifZ < 0)
        {
            movingForward = false;
            forwardRCSEnabled = true;
            rCS_BLF.Play();
            rCS_BRF.Play();
            Debug.Log("StopMoveForward");
        }
        else if (velocity.z > -rCSPrecision && velocity.z < rCSPrecision && forwardRCSEnabled)
        {
            forwardRCSEnabled = false;
            rCS_BLF.Stop();
            rCS_BRF.Stop();
            Debug.Log("RCSDisable");
        }
    }

    void BackRCS(Vector3 velocity)
    {
        float speedDifZ = Mathf.Abs(velocity.z) - Mathf.Abs(oldVelocity.z);
        if (!movingBack && velocity.z < -rCSPrecision && speedDifZ < 0)
        {
            movingBack = true;
            backRCSEnabled = false;
            rCS_BLB.Stop();
            rCS_BRB.Stop();
            Debug.Log("MoveBack");
        }
        else if (movingBack && !backRCSEnabled && speedDifZ > 0)
        {
            movingBack = false;
            backRCSEnabled = true;
            rCS_BLB.Play();
            rCS_BRB.Play();
            Debug.Log("StopMoveBack");
        }
        else if (velocity.z > -rCSPrecision && velocity.z < rCSPrecision && backRCSEnabled)
        {
            backRCSEnabled = false;
            rCS_BLB.Stop();
            rCS_BRB.Stop();
            Debug.Log("RCSDisable");
        }
        
    }


    //void TestLeftRCS(Vector3 velocity)
    //{
    //    float speedDifX = Mathf.Abs(velocity.x) - Mathf.Abs(oldVelocity.x);
    //    float speedDifZ = Mathf.Abs(velocity.z) - Mathf.Abs(oldVelocity.z);

    //    Vector3 curPosX = transform.InverseTransformDirection(transform.root.position);
    //    float posDifX = Mathf.Abs(curPosX.x) - Mathf.Abs(oldPos.x);

    //    Debug.Log(velocity.x);
            
        

    //    if (!movingLeft && velocity.x < -rCPrecision && speedDifX > 0)
    //    {
    //        movingLeft = true;
    //        leftRCSEnabled = false;
    //        rCS_FL.Stop();
    //        rCS_BL.Stop();
    //        //Debug.Log("Moveleft");
    //    }
    //    else if (movingLeft && !leftRCSEnabled && speedDifX < 0)
    //    {
    //        movingLeft = false;
    //        leftRCSEnabled = true;
    //        rCS_FL.Play();
    //        rCS_BL.Play();
    //        //Debug.Log("StopMoveLeft");
    //    }
    //    else if (velocity.x > -rCPrecision && velocity.x < rCPrecision && leftRCSEnabled)
    //    {
    //        leftRCSEnabled = false;
    //        rCS_FL.Stop();
    //        rCS_BL.Stop();
    //        //Debug.Log("RCSDisable");
    //    }

    //    oldPos.x = curPosX.x;
    //    oldVelocity.x = velocity.x;
    //    oldVelocity.z = velocity.z;
    //}




    //void TestRCSBasedOnDiferenceSpeed()
    //{
    //    Vector3 localVelocity = transform.InverseTransformDirection(rootRB.velocity);
    //    float speedDifX = Mathf.Abs(localVelocity.x) - Mathf.Abs(oldVelocity.x);
    //    float speedDifZ = Mathf.Abs(localVelocity.z) - Mathf.Abs(oldVelocity.z);

    //    if (localVelocity.x < -1 & !movingLeft & speedDifX > 0)
    //    {
    //        movingLeft = true;
    //        leftRCSEnabled = false;
    //        rCS_FL.Stop();
    //        rCS_BL.Stop();
    //        Debug.Log("Moveleft");
    //    }
    //    else if (speedDifX < 0 & movingLeft & !leftRCSEnabled)
    //    {
    //        movingLeft = false;
    //        leftRCSEnabled = true;
    //        rCS_FL.Play();
    //        rCS_BL.Play();
    //        Debug.Log("StopMoveLeft");
    //    }
    //    else if (localVelocity.x > -1 & localVelocity.x < 1 & leftRCSEnabled)
    //    {
    //        leftRCSEnabled = false;
    //        rCS_FL.Stop();
    //        rCS_BL.Stop();
    //        Debug.Log("RCSDisable");
    //    }

    //    oldVelocity.x = localVelocity.x;
    //    oldVelocity.z = localVelocity.z;
    //}
}



