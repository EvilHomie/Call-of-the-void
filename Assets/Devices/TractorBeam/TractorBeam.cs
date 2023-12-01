using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "TractorBeam", menuName = "ScriptableObjects/Devices/TractorBeam")]
public class TractorBeam : ScriptableObject, IDevice
{    
    [Header("Image and Type")]
    public Texture deviceImage;
    public DeviceType deviceType = DeviceType.TractorBeam;
    public string deviceDescription = "Device Description\r\n\r\nTractorBeam.\r\nallows you to collect resources.";

    [Header("Collect Distance")]
    public IntReactiveProperty collectDistanceLevel;
    public int collectDistanceMaxLevel;
    public int collectDistanceEachLevel;
    public  int CurrentCollectDistance => collectDistanceLevel.Value * collectDistanceEachLevel;

    [Header("Pull Speed")]
    public IntReactiveProperty pullSpeedLevel;
    public int pullSpeedMaxLevel;
    public int pullSpeedEachLevel;
    public  int CurrentPullSpeed => pullSpeedLevel.Value * pullSpeedEachLevel;


    [Header("UpgradeLists")]
    public List<Improvement> improvementsList = new();
    public List<ImprovementCost> improvementsCostList = new();


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
}
