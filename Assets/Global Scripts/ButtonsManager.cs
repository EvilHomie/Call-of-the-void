using UnityEngine;

public class ButtonsManager : MonoBehaviour
{    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerDevices.TractorBeamActiveStatus.Value = true;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            PlayerDevices.TractorBeamActiveStatus.Value = false;
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            EventBus.CommandOnTryGetTarget.Execute();
            Debug.Log("Button is Presed");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerCargo.InventoryActiveStatus.Value = !PlayerCargo.InventoryActiveStatus.Value;
            if (PlayerCargo.InventoryActiveStatus.Value)
            {
                EventBus.SelectDevice.Value = null;
                Time.timeScale = 0;
            }else Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerCargo.currentCargo.Value.improvementsList[0].improvementLevel.Value++;
            Debug.Log("slotLevelIncrease");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerCargo.currentCargo.Value.slotsCapacityLevel.Value++;
            Debug.Log("slotCapacityIncrease");
        }
    }


    //void Start()
    //{
    //    Observable.EveryUpdate() // поток update
    //      .Where(_ => Input.anyKeyDown) // фильтруем на нажатие любой клавиши
    //      .Select(_ => Input.inputString) // выбираем нажатую клавишу
    //      .Subscribe(key =>
    //      { // подписываемся
    //          OnKeyDown(key); // вызываем метод OnKeyDown c параметром нажатой клавиши
    //      }).AddTo(this); // привязываем подписку к gameobject-у
    //}

    //private void OnKeyDown(string key)
    //{
    //    switch (key) 
    //    {
    //        case 
    //    }
    //}
}
