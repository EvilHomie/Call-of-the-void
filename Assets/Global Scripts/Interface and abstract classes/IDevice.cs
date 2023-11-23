using System.Collections.Generic;
using UnityEngine;

public interface IDevice
{
    Texture GetDeviceImage();

    string GetDeviceDescription();

    List<Improvement> GetImprovements();

    List<ImprovementCost> GetImprovementsCosts();
}
