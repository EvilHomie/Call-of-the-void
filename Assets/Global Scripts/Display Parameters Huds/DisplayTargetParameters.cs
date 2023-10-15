using UnityEngine;

public class DisplayTargetParameters : DisplayParameters
{
    protected override void OnEnable()
    {
        EventBus.onSelectTarget += SetMaxParameters;
        EventBus.onDeselectTarget += TurnOffDisplay;
    }
    protected override void OnDisable()
    {
        EventBus.onSelectTarget -= SetMaxParameters;
        EventBus.onDeselectTarget -= TurnOffDisplay;
    }


    protected override void SetMaxParameters(GameObject obj)
    {
        base.SetMaxParameters(obj);
        GlobalData.targetIsSelected = true;
    }

    protected override void TurnOffDisplay()
    {
        base.TurnOffDisplay();
        GlobalData.targetIsSelected = false;
    }
}
