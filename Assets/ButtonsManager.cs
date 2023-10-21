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
            EventBus.ComandOnTryGetTarget.Execute();
            Debug.Log("Button is Presed");
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