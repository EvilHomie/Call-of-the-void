using UniRx;
using UnityEngine;

public class CollectableLogic : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    CompositeDisposable _disposable2 = new();
    Rigidbody rb;

    LineRenderer lineRenderer;

    bool tractorBeamIsActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        EventBus.TractorBeamActiveStatus.Subscribe(activeStatus => TractorBeamChangeStatusLogic(activeStatus)).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
        _disposable2.Clear();
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.CompareTag("Player"))
        {
            EventBus.CommandOnCollectResource.Execute(gameObject);
        }
    }

    void TractorBeamChangeStatusLogic(bool status)
    {
        tractorBeamIsActive = status;
        if (status && InTractorBeamRange())
        {
            TranslateToPlayer();
            lineRenderer.enabled = true;
        }
    }
    bool InTractorBeamRange()
    {
        float distance = Vector3.Distance(GlobalData.PlayerTransform.position, transform.position);
        if (distance <= GlobalData.GrabRadius) return true;
        else return false;
    }


    void TranslateToPlayer()
    {
        Observable.EveryFixedUpdate()
                .Subscribe(_ =>
                {
                    MoveToPlayer();
                    if (!tractorBeamIsActive || !InTractorBeamRange())
                    {
                        _disposable2.Clear();
                        lineRenderer.enabled = false;
                    }
                }).AddTo(_disposable2);
    }

    void MoveToPlayer()
    {
        Vector3 dir = GlobalData.PlayerTransform.position - transform.position;
        dir = dir.normalized;
        rb.AddForce(dir * GlobalData.PullSpeed, ForceMode.Acceleration);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GlobalData.PlayerTransform.position);
    }
}