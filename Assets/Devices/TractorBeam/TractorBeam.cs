using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "TractorBeam", menuName = "ScriptableObjects/Devices/TractorBeam")]
public class TractorBeam : ScriptableObject, IDevice
{    
    [Header("Image and Type")]
    [SerializeField] Texture deviceImage;
    [SerializeField] DeviceType deviceType = DeviceType.TractorBeam;
    [SerializeField] DeviceRarity deviceRarity;
    [SerializeField] string deviceDescription = "Device Description\r\n\r\nTractorBeam.\r\nallows you to collect resources.";
    [SerializeField] string deviceID;

    [Header("Collect Distance")]
    [SerializeField] IntReactiveProperty collectDistanceLevel;
    [SerializeField] int collectDistanceMaxLevel;
    [SerializeField] int collectDistanceEachLevel;
    public  int CurrentCollectDistance => collectDistanceLevel.Value * collectDistanceEachLevel;

    [Header("Pull Speed")]
    [SerializeField] IntReactiveProperty pullSpeedLevel;
    [SerializeField] int pullSpeedMaxLevel;
    [SerializeField] int pullSpeedEachLevel;
    public  int CurrentPullSpeed => pullSpeedLevel.Value * pullSpeedEachLevel;


    [Header("UpgradeLists")]
    List<Improvement> improvementsList = new();
    [SerializeField] List<ImprovementCost> improvementsCostList = new();

    [Header("Create Cost")]
    [SerializeField] List<Condition> CreateConditions;

    private void Awake()
    {
        FillImprovementsList();
    }

    public void FillImprovementsList()
    {
        improvementsList.Add(DeviceManager.GreatImprovement("Collect Distance", collectDistanceLevel, collectDistanceMaxLevel, CurrentCollectDistance, collectDistanceEachLevel));
        improvementsList.Add(DeviceManager.GreatImprovement("Pull Speed", pullSpeedLevel, pullSpeedMaxLevel, CurrentPullSpeed, pullSpeedEachLevel));
    }

    public Texture GetDeviceImage()
    {
        return deviceImage;
    }

    public string GetDeviceDescription()
    {
        return deviceDescription;
    }

    public List<Improvement> GetImprovements()
    {
        return improvementsList;
    }

    public List<ImprovementCost> GetImprovementsCosts()
    {
        return improvementsCostList;
    }

    public DeviceRarity GetDeviceRarity()
    {
        return deviceRarity;
    }

    public List<Condition> GetCreateCondition()
    {
        return CreateConditions;
    }

    public string GetName()
    {
        return $"{deviceRarity} {deviceType}";
    }

    public DeviceType GetDeviceType()
    {
        return deviceType;
    }

    public string GetDeviceID()
    {
        return deviceID;
    }
}
