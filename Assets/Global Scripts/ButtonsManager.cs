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
            PlayerDevices.InventoryActiveStatus.Value = !PlayerDevices.InventoryActiveStatus.Value;
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
