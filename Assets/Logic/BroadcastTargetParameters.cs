using System;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastTargetParameters : MonoBehaviour
{
    public static Action<float, float, float> maxParameters;
    public static Action<string, float, float, float> broadcastParameters;

    ITargetParameters targetParameters;

    GameObject targetHud;
    List <GameObject> targetHP = new();


    private void Awake()
    {
        TargetHudEnable();
    }
    private void FixedUpdate()
    {
        ParametersUpdate();
    }
    private void OnDestroy()
    {
        TargetHudDisable();
    }

    void TargetHudEnable()
    {
        targetHud = FindFirstObjectByType<DisplayTargetParameters>().gameObject;
        targetParameters = GetComponent<ITargetParameters>();
        targetParameters.GetTargetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
        maxParameters?.Invoke(maxHullHP, maxArmorHP, maxShieldHP);

        foreach (Transform child in targetHud.transform)
        {
            targetHP.Add(child.gameObject);
        }
        targetHP.ForEach(x => { x.SetActive(true); });
    }
    void TargetHudDisable()
    {
        if (FindFirstObjectByType<BroadcastTargetParameters>() == null)
        {
            targetHP.ForEach(x => { x.SetActive(false); });
        }
    }
    void ParametersUpdate()
    {
        targetParameters.GetTargetParameters(out float hullHP, out float armorHP, out float shieldHP);
        broadcastParameters?.Invoke(gameObject.name, hullHP, armorHP, shieldHP);
    }
}
