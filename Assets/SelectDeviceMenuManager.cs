using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectDeviceMenuManager : MonoBehaviour , IPointerExitHandler
{
    [SerializeField] List <Transform> devicesMenuContent;
    [SerializeField] GameObject noEvalebleDevice;
    

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }


    public void Activate<T>(List<T> deviceList)
    {
        if (deviceList.Count == 0)
        {
            noEvalebleDevice.SetActive(true);
            return;
        }
        else
        {
            noEvalebleDevice.SetActive(false);
            ActivateEvalebleDevices(deviceList);
        }
    }

    void ActivateEvalebleDevices<T>(List<T> deviceList)
    {
        for (int i = 0; i < deviceList.Count; i++)
        {
            devicesMenuContent[i].gameObject.SetActive(true);
            devicesMenuContent[i].GetComponent<DisplayEvalebleDevice>().Activate(deviceList[i]);
        }   

        for (int i = deviceList.Count; i < devicesMenuContent.Count; i++)
        {
            devicesMenuContent[i].gameObject.SetActive(false);
        }
    }   
}
