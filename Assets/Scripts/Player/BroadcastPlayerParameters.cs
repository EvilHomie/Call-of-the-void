using System;
using UnityEngine;

public class BroadcastPlayerParameters : MonoBehaviour
{
    public static Action PlayerTakeDamageEvents;

    public static Action<string, float, float, float> playerMaxParameters;
    public static Action<float, float, float> playerCurrentParameters;

    ITarget playerParameters;


    private void Start()
    {
        playerParameters = GetComponent<ITarget>();
        playerParameters.GetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
        playerMaxParameters?.Invoke(tag, maxHullHP, maxArmorHP, maxShieldHP);
    }

    private void FixedUpdate()
    {
        playerParameters.GetCurrentParameters(out float hullHP, out float armorHP, out float shieldHP);
        playerCurrentParameters?.Invoke(hullHP, armorHP, shieldHP);
    }
}
