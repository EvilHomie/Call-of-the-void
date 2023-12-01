using UnityEngine;

public class ButtonsManager : MonoBehaviour
{    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventBus.TractorBeamActiveStatus.Value = true;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            EventBus.TractorBeamActiveStatus.Value = false;
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            EventBus.CommandOnTryGetTarget.Execute();
            Debug.Log("Button is Presed");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            EventBus.InventoryActiveStatus.Value = !EventBus.InventoryActiveStatus.Value;
            if (EventBus.InventoryActiveStatus.Value)
            {
                EventBus.SelectDevice.Value = null;
                Time.timeScale = 0;
            }else Time.timeScale = 1;
        }
    }


    //void Start()
    //{
    //    Observable.EveryUpdate() // ����� update
    //      .Where(_ => Input.anyKeyDown) // ��������� �� ������� ����� �������
    //      .Select(_ => Input.inputString) // �������� ������� �������
    //      .Subscribe(key =>
    //      { // �������������
    //          OnKeyDown(key); // �������� ����� OnKeyDown c ���������� ������� �������
    //      }).AddTo(this); // ����������� �������� � gameobject-�
    //}

    //private void OnKeyDown(string key)
    //{
    //    switch (key) 
    //    {
    //        case 
    //    }
    //}
}
