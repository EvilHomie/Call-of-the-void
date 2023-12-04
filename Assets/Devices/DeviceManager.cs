using System.Collections.Generic;
using UniRx;

public static class DeviceManager
{
    public static Improvement GreatImprovement(string name, IntReactiveProperty level, int maxLevel, int curValue, int upgEffect)
    {
        Improvement improvement = new()
        {
            improvementName = name,
            improvementLevel = level,
            improvementMaxLevel = maxLevel,
            curentValue = curValue,
            upgEffect = upgEffect
        };
        return improvement;
    }
}
public enum DeviceType
{
    Engine,
    RCS,
    Shield,
    Generator,
    TractorBeam,
    RepairDrone,
    Cargo
}

public enum DeviceRarity
{
    Poor,
    Common,
    Uncommon,
    Rare,
    Epic
}

[System.Serializable]
public class Improvement
{
    public string improvementName;
    public IntReactiveProperty improvementLevel;
    public int improvementMaxLevel;
    public int curentValue;
    public int upgEffect;
}

[System.Serializable]
public class ImprovementCost
{
    public int upgradeLevel;
    public List<Condition> conditionsForUpgrade;
}

[System.Serializable]
public class Condition
{
    public ResourceType resourceType;
    public int resAmount;
}
