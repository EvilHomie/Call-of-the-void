using UniRx;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask interactLayer;
    GameObject currentTarget;

    private void OnEnable()
    {
        EventBus.CommandOnTryGetTarget.Subscribe(_ => SetTarget()).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    void SetTarget()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, interactLayer))
        {
            currentTarget = raycastHit.collider.transform.root.gameObject;

            if (currentTarget.CompareTag("Resource")) return;

            EventBus.SelectedTarget.Value = currentTarget;

            Debug.Log("Target selected");
        }
        else
        {
            EventBus.SelectedTarget.Value = null;
            Debug.Log("Target null");
        }
    }  
}
