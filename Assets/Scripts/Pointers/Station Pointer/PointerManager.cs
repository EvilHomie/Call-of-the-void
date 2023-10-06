using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] TextMeshProUGUI detectionText;
    [SerializeField] TextMeshProUGUI targetName;

    [SerializeField] List<Transform> listChildsObj = new();

    Vector3 playerPos;
    Vector3 targetPos;
    bool pointerEnabled;
    Color defaultColor;
    private void Awake()
    {
        defaultColor = detectionText.faceColor;
        Fillist();
        DisablePointer();
    }
    protected virtual void OnEnable()
    {
        EventBus.broadcastPlayerTransform += GetPlayerPos;
    }

    protected virtual void OnDisable()
    {
        EventBus.broadcastPlayerTransform -= GetPlayerPos;
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

    protected void GetTargetData(Vector3 targetPos, string targetName)
    {
        if (targetPos != Vector3.zero)
        {
            this.targetPos = targetPos;
            this.targetName.text = $">>{targetName}<<";
            EnablePointer();
            StartCoroutine(PulsingText());
        }
        else { DisablePointer(); }
    }

    protected void GetPlayerPos(Transform playerTransform)
    {
        playerPos = playerTransform.position;
    }
    void RotateToTarget()
    {
        if (pointerEnabled)
        {
            Vector3 stationDir = (targetPos - playerPos).normalized;
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
            detectionText.faceColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, (Mathf.Sin(Time.time) + 1) / 2);
            yield return null;
        }
    }
}
