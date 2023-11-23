using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Cargo", menuName = "ScriptableObjects/Devices/Cargo")]
public class Cargo : ScriptableObject, IDevice
{    
    [Header("Image and Type")]
    public Texture deviceImage;
    public DeviceType deviceType = DeviceType.Cargo;
    public string deviceDescription = "Device Description\r\n\r\nCONTAINER.\r\nallows you to store resources.";

    [Header("Slots Number")]
    public IntReactiveProperty slotsNumberLevel;
    public int slotsNumberMaxLevel;
    public int slotNumberEachLevel;
    public  int CurrentSlotsNumber => slotsNumberLevel.Value * slotNumberEachLevel;

    [Header("Slots Capacity")]
    public IntReactiveProperty slotsCapacityLevel;
    public int slotsCapacityMaxLevel;
    public int slotCapacityEachLevel;
    public  int CurrentSlotsCapacity => slotsCapacityLevel.Value * slotCapacityEachLevel;


    [Header("UpgradeLists")]
    public List<Improvement> improvementsList = new();
    public List<ImprovementCost> improvementsCostList = new();


    public void FillImprovementsList()
    {
        improvementsList.Add(GreatImprovement("Max Slot Number", slotsNumberLevel, slotsNumberMaxLevel, CurrentSlotsNumber, slotNumberEachLevel));
        improvementsList.Add(GreatImprovement("Max Slot Capacity", slotsCapacityLevel, slotsCapacityMaxLevel, CurrentSlotsCapacity, slotCapacityEachLevel));
    }

    Improvement GreatImprovement(string name, IntReactiveProperty level, int maxLevel, int curValue, int upgEffect)
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
