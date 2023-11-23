using System.Collections.Generic;
using UniRx;

public class DeviceManager
{
    
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
