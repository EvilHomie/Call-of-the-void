using UnityEngine;

public class BroadcastPlayerParameters : MonoBehaviour
{
    ITarget playerParameters;

    private void Start()
    {
        playerParameters = GetComponent<ITarget>();
        playerParameters.GetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
        EventBus.playerMaxParameters?.Invoke(tag, maxHullHP, maxArmorHP, maxShieldHP);
    }

    private void FixedUpdate()
    {
        playerParameters.GetCurrentParameters(out float hullHP, out float armorHP, out float shieldHP);
        EventBus.playerCurrentParameters?.Invoke(hullHP, armorHP, shieldHP);
    }
}
