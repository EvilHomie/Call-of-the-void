using System;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    ITarget targetParameters;
    GameObject targetHud;
    GameObject currentTarget;

    private void Awake()
    {
        targetHud = FindFirstObjectByType<DisplayTargetParameters>().gameObject;
        DisableTargetHud();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetTarget();
        }        
    }
    private void FixedUpdate()
    {
        GetCurrentTagretParameters();
    }

    void SetTarget()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue))
        {
            if (raycastHit.collider.gameObject.layer != 6)
            {
                currentTarget = raycastHit.collider.gameObject;
                EnableTargetHud();
                GetMaxTargetParameters();                
            }
            else
            {
                currentTarget = null;
            }
        }
    }       

    void EnableTargetHud()
    {
        targetHud.SetActive(true);
    }
    void DisableTargetHud()
    {
        targetHud.SetActive(false);
    }
    void GetMaxTargetParameters()
    {
        targetParameters = currentTarget.GetComponent<ITarget>();
        targetParameters.GetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
        EventBus.targetMaxParameters?.Invoke(currentTarget.tag, maxHullHP, maxArmorHP, maxShieldHP);        
    }

    void GetCurrentTagretParameters()
    {
        if (currentTarget != null)
        {
            targetParameters.GetCurrentParameters(out float hullHP, out float armorHP, out float shieldHP);
            EventBus.targetCurrentParameters?.Invoke(hullHP, armorHP, shieldHP);
            if (hullHP <= 0)
            {
                currentTarget = null;
            }
        }
        else if (targetHud.activeSelf)
        {
            DisableTargetHud();
        }        
    }
}
