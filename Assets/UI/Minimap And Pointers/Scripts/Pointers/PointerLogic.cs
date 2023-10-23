using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointerLogic : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] TextMeshProUGUI detectionText;
    [SerializeField] TextMeshProUGUI targetName;

    [SerializeField] List<Transform> listChildsObj = new();

    Vector3 targetPos;
    bool pointerEnabled;
    Color defaultColor;
    private void Awake()
    {
        defaultColor = detectionText.faceColor;
        Fillist();
        DisablePointer();
    }   

    void FixedUpdate()
    {
        RotateToTarget();
    }

    void Fillist()
    {
        foreach (Transform child in transform)
        {
            listChildsObj.Add(child);
        }
    }

    protected void SetTargetData(GameObject anomaly)
    {
        string[] name = anomaly.name.Split('('); 
        targetPos = new Vector3(anomaly.transform.position.x, 0, anomaly.transform.position.z);
        Debug.Log(targetPos);
        targetName.text = $">>{name[0]}<<";
        EnablePointer();
        StartCoroutine(PulsingText());
    }

    void RotateToTarget()
    {
        if (pointerEnabled)
        {
            Vector3 stationDir = (targetPos - GlobalData.PlayerTransform.position).normalized;
            Quaternion toStation = Quaternion.LookRotation(stationDir, Vector3.right);
            pointer.transform.rotation = Quaternion.RotateTowards(pointer.transform.rotation, toStation, 180f);
        }
    }

    protected void DisablePointer()
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
            detectionText.faceColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, (Mathf.Sin(Time.time) + 1) / 2);
            yield return null;
        }
    }
}
