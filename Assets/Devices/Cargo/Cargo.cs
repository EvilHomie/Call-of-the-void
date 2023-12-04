using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Cargo", menuName = "ScriptableObjects/Devices/Cargo")]
public class Cargo : ScriptableObject, IDevice
{    
    [Header("Image and Type")]
    [SerializeField] Texture deviceImage;
    [SerializeField] DeviceType deviceType = DeviceType.Cargo;
    [SerializeField] DeviceRarity deviceRarity;
    [SerializeField] string deviceDescription = "Device Description\r\n\r\nCONTAINER.\r\nallows you to store resources.";
    [SerializeField] string deviceID;

    [Header("Slots Amount")]
    public IntReactiveProperty slotsNumberLevel;
    [SerializeField] int slotsNumberMaxLevel;
    [SerializeField] int slotNumberEachLevel;
    public int CurrentSlotsNumber => slotsNumberLevel.Value * slotNumberEachLevel;

    [Header("Slots Capacity")]
    [SerializeField] IntReactiveProperty slotsCapacityLevel;
    [SerializeField] int slotsCapacityMaxLevel;
    [SerializeField] int slotCapacityEachLevel;
    public int CurrentSlotsCapacity => slotsCapacityLevel.Value * slotCapacityEachLevel;


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
        improvementsList.Add(DeviceManager.GreatImprovement("Slots Amount", slotsNumberLevel, slotsNumberMaxLevel, CurrentSlotsNumber, slotNumberEachLevel));
        improvementsList.Add(DeviceManager.GreatImprovement("Slots Capacity", slotsCapacityLevel, slotsCapacityMaxLevel, CurrentSlotsCapacity, slotCapacityEachLevel));
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
