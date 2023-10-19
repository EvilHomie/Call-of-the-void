using UnityEngine;

public class CollectableLogic : MonoBehaviour
{
    Rigidbody rb;    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (CheckDistance())
            {
                TranslateToPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.CompareTag("Player"))
        {
            EventBus.onCollectResource?.Invoke(gameObject);
        }
    }

    bool CheckDistance()
    {
        float distance = Vector3.Distance(GlobalData.playerTransform.position, transform.position);
        if(distance <= GlobalData.grabRadius) { return true; }
        else { return false; }
    }


    void TranslateToPlayer()
    {
        Vector3 dir = GlobalData.playerTransform.position - transform.position;
        dir = dir.normalized;
        rb.AddForce(dir * GlobalData.pullSpeed, ForceMode.Acceleration);
    }    
}
