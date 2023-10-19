using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layer;
    GameObject currentTarget;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetTarget();
        }        
    }   

    void SetTarget()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layer))
        {
            currentTarget = raycastHit.collider.transform.root.gameObject;
            EventBus.onSelectTarget?.Invoke(currentTarget);
        }
        else
        {
            EventBus.onDeselectTarget?.Invoke();
        }
    }  
}
