using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplaySelectedDevice : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] RawImage selectedDeviceImage;
    [SerializeField] TextMeshProUGUI deviceDescriptionText;
    [SerializeField] List<GameObject> deviceDeffText = new();
    [SerializeField] GameObject improvementTitleText;
    [SerializeField] Transform improvementWindow;


    private void OnEnable()
    {
        EventBus.SelectDevice.Subscribe(device =>
        {
            if (device == null)
            {
                SwitchDeffText(true);
            }
            else
            {
                SwitchDeffText(false);
                DisplayImage(device.GetDeviceImage());
                DisplayDescription(device.GetDeviceDescription());
                DisplayEvalebleImprovements(device.GetImprovements());
            }

        }).AddTo(_disposable);
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }

    private void SwitchDeffText(bool activeStatus)
    {
        deviceDeffText.ForEach(text => text.SetActive(activeStatus));

        selectedDeviceImage.gameObject.SetActive(!activeStatus);
        deviceDescriptionText.gameObject.SetActive(!activeStatus);
        improvementTitleText.SetActive(!activeStatus);
        improvementWindow.gameObject.SetActive(!activeStatus);
    }

    void DisplayImage(Texture deviceImage)
    {
        selectedDeviceImage.texture = deviceImage;
    }

    void DisplayDescription(string deviceDescription)
    {
        deviceDescriptionText.text = deviceDescription;
    }

    void DisplayEvalebleImprovements(List<Improvement> improvements)
    {
        foreach (Transform improvLine in improvementWindow)
        {
            if (improvLine.GetSiblingIndex() < improvements.Count) improvLine.gameObject.SetActive(true);
            else improvLine.gameObject.SetActive(false);
        }
    }
}

