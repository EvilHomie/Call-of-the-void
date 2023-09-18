using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StationPointerManager : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] List<Transform> listChildsObj= new();

    Vector3 playerPos;
    Vector3 stationPos;
    bool pointerEnabled;


    private void Awake()
    {
        Fillist();
        DisablePointer();
    }
    private void OnEnable()
    {
        PlayerControl.broadcastPlayerTransform += GetPlayerPos;
        BroadcastStationPosition.broadcastStationPosition += GetStationPos;               
    }

    private void OnDisable()
    {
        PlayerControl.broadcastPlayerTransform -= GetPlayerPos;
        BroadcastStationPosition.broadcastStationPosition -= GetStationPos;
    }

    private void FixedUpdate()
    {
        RotateToAnomaly();
    }

    void Fillist()
    {
        foreach (Transform child in transform)
        {
            listChildsObj.Add(child);
        }
    }

    void GetStationPos(Vector3 stationPos)
    {
        if (stationPos != Vector3.zero)
        {
            this.stationPos = stationPos;
            EnablePointer();
            StartCoroutine(PulsingText());
        }
        else { DisablePointer(); }
    }

    void GetPlayerPos(Transform playerTransform)
    {
        playerPos = playerTransform.position;
    }
    void RotateToAnomaly()
    {
        if (pointerEnabled)
        {
            listChildsObj.ForEach(child => child.gameObject.SetActive(true));
            Vector3 stationDir = (stationPos - playerPos).normalized;
            Quaternion toStation = Quaternion.LookRotation(stationDir, Vector3.right);
            pointer.transform.rotation = Quaternion.RotateTowards(pointer.transform.rotation, toStation, 180f);
        }     
    }

    void DisablePointer()
    {
        pointerEnabled = false;
        listChildsObj.ForEach(child => child.gameObject.SetActive(false));
    }
    void EnablePointer()
    {
        pointerEnabled = true;
        listChildsObj.ForEach(child => child.gameObject.SetActive(true));
    }

    IEnumerator PulsingText()
    {
        while (pointerEnabled)
        {
            text.faceColor = new Color(text.faceColor.r, text.faceColor.g, text.faceColor.b, (Mathf.Sin(Time.time) + 1) / 2);
            yield return null;
        }
    }
}
