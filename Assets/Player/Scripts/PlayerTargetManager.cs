using System;
using UnityEngine;

public class PlayerTargetManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    public static Action<string, float, float, float> targetMaxParameters;
    public static Action<float, float, float> broadcastTargetParameters;

    ITargetParameters targetParameters;

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
            if (raycastHit.collider.gameObject.layer != 6 & !raycastHit.collider.gameObject.CompareTag("Player"))
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
        targetParameters = currentTarget.GetComponent<ITargetParameters>();
        targetParameters.GetTargetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
        targetMaxParameters?.Invoke(currentTarget.tag, maxHullHP, maxArmorHP, maxShieldHP);        
    }

    void GetCurrentTagretParameters()
    {
        if (currentTarget != null)
        {
            targetParameters.GetTargetParameters(out float hullHP, out float armorHP, out float shieldHP);
            broadcastTargetParameters?.Invoke(hullHP, armorHP, shieldHP);
        }
        else
        {
            DisableTargetHud();
        }        
    }
}
