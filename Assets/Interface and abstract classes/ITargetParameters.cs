using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetParameters
{
    public void GetTargetParameters(out float hullHP, out float armorHP, out float shieldHP);
    public void GetTargetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
}

